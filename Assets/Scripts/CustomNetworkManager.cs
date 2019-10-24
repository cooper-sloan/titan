using System;
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
    public GameObject mobileNetworkUi;

    public enum PlayerType {VrPlayerType, MobilePlayerType};
    //subclass for sending network messages
    public class PlayerTypeMessage : MessageBase
    {
        public PlayerType playerType;
    }


    // Start is called before the first frame update
    public void StartHosting()
    {
        networkDiscovery.Initialize();
        networkDiscovery.StartAsServer();
        base.StartHost();
    }

    public void StartClientConnection()
    {
        networkDiscovery.Initialize();
        networkDiscovery.StartAsClient();
    }

    public void StartServerOnly()
    {
        base.StartServer();
    }

    // Client callbacks
    public override void OnClientConnect(NetworkConnection conn)
    {
        PlayerTypeMessage playerTypeMessage = new PlayerTypeMessage();
        if (XRDevice.isPresent){
            playerTypeMessage.playerType = PlayerType.VrPlayerType;
        } else {
            playerTypeMessage.playerType = PlayerType.MobilePlayerType;
        }
        ClientScene.AddPlayer(conn, 0, playerTypeMessage);
        mobileNetworkUi.SetActive(false);
    }
    public override void OnStartServer(){
    }


    public override void OnClientDisconnect(NetworkConnection conn)
    {
        if (XRDevice.isPresent)
        {
           // TODO: show vr lobby ui
        }
        else
        {
            mobileNetworkUi.SetActive(true);
        }

        base.OnClientDisconnect(conn);
    }

    // Server callbacks
    public override void OnServerConnect(NetworkConnection conn)
    {
        Debug.Log("ZZZ A client connected to the server: " + conn);
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader){
        PlayerTypeMessage playerTypeMessage = extraMessageReader.ReadMessage<PlayerTypeMessage>();
        PlayerType playerType = playerTypeMessage.playerType;
        if (playerType == PlayerType.VrPlayerType){
            var player = (GameObject)GameObject.Instantiate(vrPlayer, Vector3.zero, Quaternion.identity);
            NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
        } else if(playerType == PlayerType.MobilePlayerType){
            var player = (GameObject)GameObject.Instantiate(mobilePlayer, Vector3.zero, Quaternion.identity);
            NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
        }
    }


}
