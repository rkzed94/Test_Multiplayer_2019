using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using MultiPlayerGame.Game;

public class MainMenu : MonoBehaviourPunCallbacks
{

    private string _nickname;
    private string _roomName;

    enum MainMenuOption
    {
        STARTGAME,
        CREATEROOM,
        JOINROOM
    }

    MainMenuOption _option;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Connecting to Server..");
        PhotonNetwork.ConnectUsingSettings();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool Btn_Close() { return true; }

    public void Btn_CreateRoom() //* Create and join the lobby.
    {
        _option = MainMenuOption.CREATEROOM;
    }

    public void Btn_JoinRoom() //* join already created lobby.
    {
        _option = MainMenuOption.JOINROOM;
    }

    public void Btn_StartGame() //* Join random lobby and if no lobby available, create one.
    {
        _option = MainMenuOption.STARTGAME;
    }

    public void Btn_StartMultiplayer()
    {
        if (_nickname.Length > 0) //* check if nickname value is not null;
        {
            PhotonNetwork.NickName = _nickname; //* Set player nickname;

            RoomOptions _ro = new RoomOptions(); //* Set Room property
            _ro.MaxPlayers = 4;

            switch (_option)
            {
                case MainMenuOption.STARTGAME:
                    PhotonNetwork.JoinRandomRoom();
                    break;
                case MainMenuOption.CREATEROOM:
                    PhotonNetwork.CreateRoom(_roomName, _ro, TypedLobby.Default);
                    break;
                case MainMenuOption.JOINROOM:
                    PhotonNetwork.JoinRoom(_roomName);
                    break;
            }
        }
    }

    public void InputNickName(string msg)
    {
        _nickname = msg;
    }

    public void InputRoomName(string msg)
    {
        _roomName = msg;
    }


    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to master");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogError("Unable to connect to master, probably offline?");

    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Fails to join random lobby, create new one!");
        RoomOptions _ro = new RoomOptions();
        _ro.MaxPlayers = 4;
        PhotonNetwork.CreateRoom(null, _ro, TypedLobby.Default);
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("Successfully joined a Room: " + PhotonNetwork.CurrentRoom);
        PhotonNetwork.LoadLevel(1);
    }
}
