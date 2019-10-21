using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class CustomNetworkDiscovery : NetworkDiscovery
{

    public void Start()
    {
        Initialize();
    }
    public override void OnReceivedBroadcast(string fromAddress, string data)
    {
        Debug.Log("On recieved broadcast with address");
        NetworkManager.singleton.networkAddress = fromAddress;
        NetworkManager.singleton.StartClient();
    }
}
