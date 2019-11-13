using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holdable : MonoBehaviour
{

		public GameObject parent;
		public float height = 2.5f;
		Vector3 startPos;
		ParticleSystem particleSystem;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        particleSystem = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
    	if (parent != null){
    		transform.position = Vector3.Lerp(transform.position,parent.transform.position + (Vector3.up * height),.8f);
    	}
        
    }



   // Todo: add logic for dropping. This could be done by user or by being hit.
   public void Drop(){

   }

   public void ResetPosition(){
      parent = null;
      transform.position = startPos;
      Rigidbody rb = GetComponent<Rigidbody>();
   		rb.isKinematic = false;
   }

   public void PickUp(GameObject grabber){
   	Rigidbody rb = GetComponent<Rigidbody>();
   	rb.isKinematic = true;
   	parent = grabber;
   }
}
