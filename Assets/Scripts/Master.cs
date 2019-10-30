using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Mirror;
public class Master : MonoBehaviour
{
    public CustomNetworkManager networkManager;
    public GameObject mobileUI;
    public GameObject vrPlayer;
    public GameObject mobileMenuCamera;

    void Awake(){
        if (XRDevice.isPresent){
            // enable vr player
            vrPlayer.SetActive(true);
        } else{
            mobileUI.SetActive(true);
            mobileMenuCamera.SetActive(true);
        }
    }
    void Start(){
        if (XRDevice.isPresent){
            networkManager.StartHosting();
       }
    }
}

