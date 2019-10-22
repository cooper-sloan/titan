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
        base.OnClientConnect(conn);
        Debug.Log("Connected successfully to server, now to set up other stuff for the client...");
    }

    // Server callbacks
    public override void OnServerConnect(NetworkConnection conn)
    {
        Debug.Log("A client connected to the server: " + conn);
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId){
        if (XRDevice.isPresent){
            NetworkServer.AddPlayerForConnection(conn, vrPlayer, playerControllerId);
        } else{
            var player = (GameObject)GameObject.Instantiate(mobilePlayer, Vector3.zero, Quaternion.identity);
            NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
        }
    }


}
