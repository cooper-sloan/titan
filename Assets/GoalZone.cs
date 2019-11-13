using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalZone : MonoBehaviour
{

		ParticleSystem particleSystem;
		
    // Start is called before the first frame update
    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col){
    	Debug.Log("Goal zone emitting");
    	//particleSystem.Play();
    	particleSystem.Emit(1000);
      particleSystem.Play();

    }


}
