using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using MultiPlayerGame.Game;

public class Chat : MonoBehaviourPunCallbacks
{


    public class ChatMessage
    {
        public string sender = "";
        public string message = "";
        public Text textObject;

    }

    public GameObject _chatPanel;
    public GameObject _textObj;
    public GameObject _inputFieldUi;

    private string _chat;
    private PhotonView _photonView;

    List<ChatMessage> _chatMessages = new List<ChatMessage>();


    private void Start()
    {
        _photonView = PhotonView.Get(this);
        _photonView.RPC("SendChat", RpcTarget.AllBuffered, "System", PhotonNetwork.NickName + " joined room..");
    }

    public void InputChat(string msg)
    {
        _chat = msg;
    }
    public void SubmitChat()
    {
        _photonView.RPC("SendChat", RpcTarget.All, PhotonNetwork.NickName, _chat);
        InputField _inputfield = _inputFieldUi.GetComponent<InputField>();
        _inputfield.text = ""; //* Clear input field

    }

    public override void OnPlayerLeftRoom(Player player)
    {
        SendChat("System", player.NickName + " leave room..");
    }

    [PunRPC]
    void SendChat(string sender, string message)
    {
        ChatMessage msg = new ChatMessage();
        msg.sender = sender;
        msg.message = sender + " : " + message;
        GameObject textObject = Instantiate(_textObj, _chatPanel.transform);
        msg.textObject = textObject.GetComponent<Text>();
        msg.textObject.text = msg.message;
        _chatMessages.Insert(0, msg);

    }
}
