using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class CustomNetworkDiscovery : NetworkDiscovery
{
    NetworkManager manager;
    public override void OnReceivedBroadcast(string fromAddress, string data)
    {
        NetworkManager manager = GetComponent<NetworkManager>();
        Debug.Log("ZZZ On recieved broadcast with address");
        manager.networkAddress = fromAddress;
        StopBroadcast();
        manager.StartClient();
    }
}
