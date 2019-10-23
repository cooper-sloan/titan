﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.XR;

public class CustomNetworkManager : NetworkManager
{
    public CustomNetworkDiscovery networkDiscovery;
    public GameObject vrPlayer;
    public GameObject mobilePlayer;

    public enum PlayerType {VrPlayerType, MobilePlayerType};
    //subclass for sending network messages
    public class PlayerTypeMessage : MessageBase
    {
        public PlayerType playerType;
    }


    // Start is called before the first frame update
    public void StartHosting()
    {
        Debug.Log("Start hosting!");
        networkDiscovery.Initialize();
        networkDiscovery.StartAsServer();
        base.StartHost();
    }

    public void StartClientConnection()
    {
        Debug.Log("Start client!");
        networkDiscovery.Initialize();
        networkDiscovery.StartAsClient();
    }

    public void StartServerOnly()
    {
        Debug.Log("Start server!");
        base.StartServer();
    }

    // Client callbacks
    public override void OnClientConnect(NetworkConnection conn)
    {
        PlayerTypeMessage playerTypeMessage = new PlayerTypeMessage();
        if (XRDevice.isPresent){
            playerTypeMessage.playerType = PlayerType.VrPlayerType;
            Debug.Log("ZZZ VR");
        } else {
            Debug.Log("ZZZ mobile");
            playerTypeMessage.playerType = PlayerType.MobilePlayerType;
        }
        Debug.Log("ZZZ OnClientConnect adding player");
        ClientScene.AddPlayer(conn, 0, playerTypeMessage);
        Debug.Log("Connected successfully to server, now to set up other stuff for the client...");
    }

    // Server callbacks
    public override void OnServerConnect(NetworkConnection conn)
    {
        Debug.Log("A client connected to the server: " + conn);
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader){
        Debug.Log("ZZZ OnServerAddPlayer adding player");
        PlayerTypeMessage playerTypeMessage = extraMessageReader.ReadMessage<PlayerTypeMessage>();
        PlayerType playerType = playerTypeMessage.playerType;
        Debug.Log(String.Format("ZZZ {0:G}", playerType));
        if (playerType == PlayerType.VrPlayerType){
            var player = (GameObject)GameObject.Instantiate(vrPlayer, Vector3.zero, Quaternion.identity);
            NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
        } else if(playerType == PlayerType.MobilePlayerType){
            var player = (GameObject)GameObject.Instantiate(mobilePlayer, Vector3.zero, Quaternion.identity);
            NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
        }
    }
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId){
        Debug.Log("ZZZ penis");
    }


}
