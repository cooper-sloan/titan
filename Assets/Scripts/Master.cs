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
            vrPlayer.SetActive(true);
        } else{
            mobileUI.SetActive(true);
            GameObject.Find("MobileMenuCamera").SetActive(true);
        }
    }
    void Start(){
        if (XRDevice.isPresent){
            networkManager.StartClientConnection();
        }
    }
}

