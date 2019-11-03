using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    public float health = 100f;

    void Start(){
    }
    void Update(){
    }

    public void TakeDamage(float damage)
    {
        Debug.LogFormat("ZZZ Destructable take damage {0} to {1}",damage.ToString(),gameObject.name.ToString());
        health -= damage;
        // If player, hurt player/kill player
        MobilePlayer mobilePlayer = GetComponent<MobilePlayer>();
        if (mobilePlayer != null)
        {
            mobilePlayer.TakeDamage(damage);
        }

        // If environment, kill environment

    }
}

