using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour {
    public float speed;
    public GameObject planet;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Orbit_();
	}

    void Orbit_()
    {
        transform.RotateAround(planet.transform.position, Vector3.up, speed * Time.deltaTime);
    }
}
