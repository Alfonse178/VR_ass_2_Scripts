using UnityEngine;
using System.Collections;


//APPLY THIS SCRUPT TO ALL OBJECTS THAT YOU WANT TO BE EFFECTED BY THE GRAVITY ATTRACTOR
//it must always have a rigidbody attached or else it wont work...it is required.

[RequireComponent (typeof (Rigidbody))]
public class GravityBody : MonoBehaviour {
	
    //referencing the GravityAttractor script.
	GravityAttractor planet;
	Rigidbody rigidbody;
	
	void Awake () {
        //Started to use tags, they are the best.
		planet = GameObject.FindGameObjectWithTag("Planet").GetComponent<GravityAttractor>();
		rigidbody = GetComponent<Rigidbody> ();

		// We have our own gravity...kind of the whole point. Disabling the in built gravity is required.
		rigidbody.useGravity = false;
        //Same as before, we want to do our own rotation.
		rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
	}
	
	void FixedUpdate () {
		// Basically allowing it to be transformed. 
		planet.Attract(rigidbody);
	}
}