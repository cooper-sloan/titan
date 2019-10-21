﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CustomNetworkManager : NetworkManager
{
    // Start is called before the first frame update
    public void StartHosting()
    {
        Debug.Log("Start hosting!");
        base.StartHost();   
    }

    public void StartClientConnnection()
    {
        Debug.Log("Start client!");
        base.StartClient();
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


}