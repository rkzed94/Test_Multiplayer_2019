using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace MultiPlayerGame.Game
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        //* Create Player
        PlayerProperty[] Players;


        public bool joinedTeam;
        public int joinedSlot;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

        }
        void Start()
        {
            //* Initiate 4 Players
            Players = new PlayerProperty[4];

        }

        public PlayerProperty getPlayerInfo(int index)
        {
            return Players[index];
        }

        public PlayerProperty getPlayerInfobyUserid(int userid)
        {
            foreach (PlayerProperty player in Players)
            {
                if (player != null)
                {
                    if (player.userid == userid)
                    {
                        return player;
                    }
                }

            }
            return new PlayerProperty();
        }
        public void removeTeam(int slot)
        {
            //* Remove Player from Team
            Players[slot] = null;

        }
        public void setTeam(int slot, PlayerProperty player)
        {
            Players[slot] = player;
        }

    }
}