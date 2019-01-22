using UnityEngine;
using System.Collections;

public class GravityAttractor : MonoBehaviour {

    //APPLY THIS SCRIPT TO THE PLANETARY OBJECTS
    //This script is responsible for the gravitational forces felt by different planets.
	
    //a float for gravitational force. Public so that it can be changed for different planets.
	public float gravity = -9.8f;

    public void Attract(Rigidbody body)
    {
        //getting a direction for the body that is normalized so that it is always the same length.
        Vector3 gravityUp = (body.position - transform.position).normalized;
        Vector3 localUp = body.transform.up;

        // Actually applying the graviational force to the body.
        body.AddForce(gravityUp * gravity);
        // Making sure that the bodies that are being affected are alligned with the centre of the planet.
        body.rotation = Quaternion.FromToRotation(localUp, gravityUp) * body.rotation;
    }
}
