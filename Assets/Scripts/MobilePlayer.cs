using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.Networking;

public class MobilePlayer : NetworkBehaviour
{
    public override void OnStartLocalPlayer(){
        GameObject.Find("MobileMenuCamera").SetActive(false);
    }
    public override void OnStartClient(){
    }
}

