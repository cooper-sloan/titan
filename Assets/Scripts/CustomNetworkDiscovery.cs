﻿using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class CustomNetworkDiscovery : NetworkDiscovery
{
    public override void OnReceivedBroadcast(string fromAddress, string data)
    {
        Debug.Log("ZZZ On recieved broadcast with address");
        NetworkManager.singleton.networkAddress = fromAddress;
        StopBroadcast();
        NetworkManager.singleton.StartClient();
    }
}
