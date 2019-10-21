using System.Collections;
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


}
