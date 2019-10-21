using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.Networking;
public class Master : MonoBehaviour
{
    public CustomNetworkManager networkManager;
    public GameObject mobileUI;
    public GameObject vrPlayer;
    public GameObject mobilePlayer;

    void Awake(){
        if (XRDevice.isPresent){
            // enable vr player
            mobileUI.SetActive(false);
            vrPlayer.SetActive(true);
            networkManager.StartHosting();
        } else{
            // enable mobile player
            mobilePlayer.SetActive(true);
        }
    }
}

