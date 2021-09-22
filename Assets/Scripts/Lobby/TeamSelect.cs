using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using MultiPlayerGame.Game;


namespace MultiPlayerGame.Lobby
{
    public class TeamSelect : MonoBehaviour
    {

        public struct Slot
        {
            public GameObject banner;
            public GameObject bannerSlot;

        }

        private Slot[] slots;

        public GameObject _slot1Obj;
        public GameObject _slot2Obj;
        public GameObject _slot3Obj;
        public GameObject _slot4Obj;

        private PlayerProperty _activePlayer;
        private GameObject _banner;
        private Image _bannerImg;
        private PhotonView _photonView;



        void Start()
        {

            _photonView = PhotonView.Get(this);
            slots = new Slot[4];

            slots[0].bannerSlot = _slot1Obj;
            slots[1].bannerSlot = _slot2Obj;
            slots[2].bannerSlot = _slot3Obj;
            slots[3].bannerSlot = _slot4Obj;

        }

        public void JoinTeam(int slot)
        {
            int team;
            if (!GameManager.Instance.joinedTeam)
            {
                if (slot >= 1)
                {
                    team = 1;
                }
                else
                {
                    team = 2;
                }

                //* safe slot and team state info to GameManager 
                GameManager.Instance.joinedSlot = slot;
                GameManager.Instance.joinedTeam = true;
                _photonView.RPC("JoinTeamRPC", RpcTarget.All, CharacterManager.Instance.selectedChara, slot, team, PhotonNetwork.NickName);
            }

        }
        public void LeaveTeam()
        {

            GameManager.Instance.joinedTeam = false;
            _photonView.RPC("LeaveTeamRPC", RpcTarget.All, GameManager.Instance.joinedSlot);

        }

        [PunRPC]
        void JoinTeamRPC(int charaid, int slot, int team, string nickname)
        {
            //* getSelectedChara
            CharacterProperty getChara = CharacterManager.Instance.characters[charaid];

            _banner = Instantiate(Resources.Load<GameObject>("Prefabs/Banner"), slots[slot].bannerSlot.transform);
            _banner.transform.Find("Image").GetComponent<Image>().sprite = getChara.image;
            _banner.transform.Find("Text").GetComponent<Text>().text = nickname;
            slots[slot].banner = _banner;


            //* createPlayer
            PlayerProperty player = new PlayerProperty();
            player.nickname = nickname;
            player.team = team;
            player.slot = slot;
            player.property = getChara;
            player.banner = _banner;
            GameManager.Instance.setTeam(slot, player);
        }

        [PunRPC]
        void LeaveTeamRPC(int slot)
        {
            GameManager.Instance.removeTeam(slot);
            Destroy(slots[slot].banner); //* destroy game object from scene
        }

    }
}