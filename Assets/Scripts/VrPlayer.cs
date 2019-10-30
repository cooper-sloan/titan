using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.Networking;

public class VrPlayer : NetworkBehaviour
{
    GameObject localLeftHand;
    GameObject leftHand;
    GameObject lightningBolt;
    public GameObject leftHandPrefab;
    public GameObject lightningPrefab;

    public override void OnStartLocalPlayer(){
        localLeftHand = GameObject.Find("CustomHandLeft");
        CmdInstantiateHand();
        CmdInstantiateLightning();
    }

    [Command]
    void CmdInstantiateHand(){
        leftHand = (GameObject)GameObject.Instantiate(leftHandPrefab);
        NetworkServer.Spawn(leftHand);
    }

    [Command]
    void CmdInstantiateLightning()
    {
        lightningBolt = (GameObject)GameObject.Instantiate(lightningPrefab);
        lightningBolt.transform.position = new Vector3(0.0f, 1.2f, 0.0f);
        NetworkServer.SpawnWithClientAuthority(lightningBolt,connectionToClient);
    }

    public void Update(){
        if(!isLocalPlayer){
            return;
        }
        CmdUpdatePosition(localLeftHand.transform.position, localLeftHand.transform.rotation);
        CmdUpdateLightningPosition(lightningBolt.transform.position, lightningBolt.transform.rotation); // Glitchy because OVRGrabber updates in FixedUpdate
    }

    [ClientRpc]
    void RpcUpdateLightningPosition(GameObject lightningBolt, Vector3 position, Quaternion rotation)
    {
        if (!isLocalPlayer)
        {
            lightningBolt.transform.position = position;
            lightningBolt.transform.rotation = rotation;
        }
       
    }

    [Command]
    public void CmdUpdateLightningPosition(Vector3 position, Quaternion rotation)
    {
        lightningBolt.GetComponent<NetworkIdentity>().AssignClientAuthority(connectionToClient);    // assign authority to the player who is changing the color
        RpcUpdateLightningPosition(lightningBolt, position, rotation);
        lightningBolt.GetComponent<NetworkIdentity>().RemoveClientAuthority(connectionToClient);    // remove the authority from the player who changed the color
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

