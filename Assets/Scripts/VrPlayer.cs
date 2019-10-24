using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.Networking;

public class VrPlayer : NetworkBehaviour
{
    GameObject localLeftHand;
    GameObject leftHand;
    public GameObject leftHandPrefab;

    void Start(){
        localLeftHand = GameObject.Find("CustomHandLeft");
        if (isLocalPlayer){
            CmdInstantiateHand();
        }
    }

    [Command]
    void CmdInstantiateHand(){
        Debug.Log("ZZZ running instantiate");
        if (isServer){
            Debug.Log("ZZZ server");
            leftHand = (GameObject)GameObject.Instantiate(leftHandPrefab);
            NetworkServer.Spawn(leftHand);
        }
    }

    public void Update(){
        if(!isLocalPlayer){
            return;
        }
        CmdUpdatePosition(localLeftHand.transform.position, localLeftHand.transform.rotation);
    }

    [Command]
    public void CmdUpdatePosition(Vector3 position, Quaternion rotation){
        leftHand.transform.position = position;
        leftHand.transform.rotation = rotation;
    }
}

