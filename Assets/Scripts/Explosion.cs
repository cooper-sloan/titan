using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Explosion : MonoBehaviour
{
    public GameObject explosionPrefab;
    public float damage = 102f;

    void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        Destructable destructable = collision.gameObject.GetComponent<Destructable>();
        if ( destructable!= null){
            Debug.LogFormat("ZZZ Explosion call take damage {0} to {1}", damage.ToString(), gameObject.name.ToString());
            destructable.TakeDamage(damage);
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3 pos = contact.point;
            GameObject explosion = Instantiate(explosionPrefab, pos, rot);
            NetworkServer.Spawn(explosion);
            Destroy(gameObject);
        }
    }
}
