using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.Networking;

public class MobilePlayer : NetworkBehaviour
{
    public GameObject localCamera;
    void Start(){
        Debug.Log("ZZZ starting MobilePlayer");
    }

    public override void OnStartLocalPlayer(){
        Debug.Log("ZZZ Added mobile player.");
        GameObject.Find("MobileMenuCamera").SetActive(false);
        localCamera.SetActive(true);
    }

    public override void OnStartClient(){
    }
}

