using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.Networking;

public class VrPlayer : NetworkBehaviour
{
    GameObject camera;

    void Start(){
        Debug.Log("ZZZ starting VrPlayer");
    }

    public override void OnStartLocalPlayer(){
        camera = GameObject.Find("CustomHandLeft");
        gameObject.transform.position = camera.transform.position;
    }
    public void Update(){
        if(isLocalPlayer){
            gameObject.transform.position = camera.transform.position;
        }
    }

    public override void OnStartClient(){
    }
}

