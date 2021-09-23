using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using MultiPlayerGame.Game;


namespace MultiPlayerGame.Lobby
{
    public class TeamSelect : MonoBehaviourPunCallbacks
    {

        public struct Slot
        {
            public GameObject banner;
            public GameObject bannerSlot;
            public GameObject readyBanner;

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

        private int _readyCount = 0;
        private bool _startGame = false;



        void Start()
        {

            _photonView = PhotonView.Get(this);
            slots = new Slot[4];

            slots[0].bannerSlot = _slot1Obj;
            slots[1].bannerSlot = _slot2Obj;
            slots[2].bannerSlot = _slot3Obj;
            slots[3].bannerSlot = _slot4Obj;

        }

        private void Update()
        {
            if (_readyCount == 4)
            {
                if (!_startGame)
                {
                    //* Start game if everyone Ready!
                    _startGame = true;
                    _photonView.RPC("StartGameRPC", RpcTarget.All);
                }
            }
        }

        public void JoinTeamfromCharaSelect(int num)
        {
            if (GameManager.Instance.joinedTeam)
            {

                JoinTeam(GameManager.Instance.joinedSlot);
            }
        }

        public void JoinTeam(int slot)
        {
            int team;

            if (GameManager.Instance.joinedTeam)
            {
                if (GameManager.Instance.joinedSlot != slot)
                {
                    return;
                }
            }

            if (slot >= 1)
            {
                team = 1;
            }
            else
            {
                team = 2;
            }

            //* Display ChangeTeamBtn
            transform.Find("ChangeTeamBtn").gameObject.SetActive(true);

            //* safe slot and team state info to GameManager 
            GameManager.Instance.joinedSlot = slot;
            GameManager.Instance.joinedTeam = true;
            _photonView.RPC("JoinTeamRPC", RpcTarget.AllBuffered, PhotonNetwork.LocalPlayer.ActorNumber, CharacterManager.Instance.selectedChara, slot, team, PhotonNetwork.NickName);



        }
        public void LeaveTeam()
        {
            //* Hide ChangeTeamBtn
            transform.Find("ChangeTeamBtn").gameObject.SetActive(false);

            GameManager.Instance.joinedTeam = false;
            _photonView.RPC("LeaveTeamRPC", RpcTarget.AllBuffered, GameManager.Instance.joinedSlot);

        }

        public void ReadyBtn()
        {

            if (GameManager.Instance.joinedTeam) //* allow click "Ready Button" only when player already joined a team
            {

                //* hide ready button and show "waiting player" message
                transform.Find("Panel").gameObject.transform.Find("ReadyBtn").gameObject.SetActive(false);
                transform.Find("Panel").gameObject.transform.Find("Stage2").gameObject.SetActive(true);

                //* hide change team button
                transform.Find("ChangeTeamBtn").gameObject.SetActive(false);

                //* show unready button
                transform.Find("UnReadyBtn").gameObject.SetActive(true);

                //*block character select
                transform.parent.gameObject.transform.Find("CharacterSelect").transform.Find("BlockCharaSelect").gameObject.SetActive(true);

                //* show ready banner
                _photonView.RPC("ReadyRPC", RpcTarget.AllBuffered, GameManager.Instance.joinedSlot);

            }

        }
        public void UnReadyBtn()
        {

            //* do the opposite of ready button

            transform.Find("Panel").gameObject.transform.Find("ReadyBtn").gameObject.SetActive(true);
            transform.Find("Panel").gameObject.transform.Find("Stage2").gameObject.SetActive(false);

            transform.Find("ChangeTeamBtn").gameObject.SetActive(true);
            transform.Find("UnReadyBtn").gameObject.SetActive(false);

            transform.parent.gameObject.transform.Find("CharacterSelect").transform.Find("BlockCharaSelect").gameObject.SetActive(false);

            _photonView.RPC("UnReadyRPC", RpcTarget.AllBuffered, GameManager.Instance.joinedSlot);

        }


        public override void OnPlayerLeftRoom(Player player)
        {

            //* detech other player if they logour or left room; then remove remaining data;

            Debug.Log("player logout");
            PlayerProperty _getPlayer = GameManager.Instance.getPlayerInfobyUserid(player.ActorNumber);

            if (_getPlayer.banner != null)
            {
                UnReadyRPC(_getPlayer.slot);
                LeaveTeamRPC(_getPlayer.slot);
            }


        }

        [PunRPC]
        void JoinTeamRPC(int userid, int charaid, int slot, int team, string nickname)
        {
            //* getSelectedChara
            CharacterProperty getChara = CharacterManager.Instance.characters[charaid];

            if (slots[slot].banner != null)
            {
                Destroy(slots[slot].banner);
            }

            _banner = Instantiate(Resources.Load<GameObject>("Prefabs/Lobby/Banner"), slots[slot].bannerSlot.transform);
            _banner.transform.Find("Image").GetComponent<Image>().sprite = getChara.image;
            _banner.transform.Find("Text").GetComponent<Text>().text = nickname;
            slots[slot].banner = _banner;


            //* createPlayer
            PlayerProperty player = new PlayerProperty();
            player.userid = userid;
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

        [PunRPC]
        void ReadyRPC(int slot)
        {
            //* initiate banner object
            slots[slot].readyBanner = Instantiate(Resources.Load<GameObject>("Prefabs/Lobby/Ready"), slots[slot].bannerSlot.transform);

            //* Increase ready count number
            _readyCount++;
        }

        [PunRPC]
        void StartGameRPC()
        {
            transform.Find("UnReadyBtn").gameObject.SetActive(false);
            transform.Find("Panel").transform.Find("Stage2").transform.Find("Text").GetComponent<Text>().text = "Starting Game \n....";


            //* Start game logic goes here!!!



        }

        [PunRPC]
        void UnReadyRPC(int slot)
        {
            //* destroy banner object
            Destroy(slots[slot].readyBanner);

            //* Decrease ready count number
            _readyCount--;
        }


    }
}