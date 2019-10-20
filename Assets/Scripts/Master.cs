using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
public class Master : MonoBehaviour
{
    public GameObject vrPlayer;
    public GameObject mobilePlayer;

    void Awake(){
        if (XRDevice.isPresent){
            // enable vr player
            Debug.Log("vr coop is yag");
            vrPlayer.SetActive(true);
        } else{
            // enable mobile player
            Debug.Log("cooop is mobile personz");
            mobilePlayer.SetActive(true);
        }
    }
}

