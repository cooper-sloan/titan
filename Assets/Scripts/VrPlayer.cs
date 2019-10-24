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

    public override void OnStartLocalPlayer(){
        localLeftHand = GameObject.Find("CustomHandLeft");
        CmdInstantiateHand();
    }

    [Command]
    void CmdInstantiateHand(){
        leftHand = (GameObject)GameObject.Instantiate(leftHandPrefab);
        NetworkServer.Spawn(leftHand);
    }

    public void Update(){
        if(!isLocalPlayer){
            return;
        }
        CmdUpdatePosition(localLeftHand.transform.position, localLeftHand.transform.rotation);
    }

    [ClientRpc]
    void RpcUpdatePosition(GameObject leftHand, Vector3 position, Quaternion rotation){
        leftHand.transform.position = position;
        leftHand.transform.rotation = rotation;
    }

    [Command]
    public void CmdUpdatePosition(Vector3 position, Quaternion rotation){
        leftHand.GetComponent<NetworkIdentity>().AssignClientAuthority(connectionToClient);    // assign authority to the player who is changing the color
        RpcUpdatePosition(leftHand, position, rotation);
        leftHand.GetComponent<NetworkIdentity>().RemoveClientAuthority(connectionToClient);    // remove the authority from the player who changed the color
    }
}

