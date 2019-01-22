using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneSpawner : MonoBehaviour {

    public GameObject myPrefab;
    GameObject myClone;
   

	// Use this for initialization
	void Update () {
        if (myClone == null) {
            myClone = Instantiate(myPrefab, new Vector3(0f, 103.4404f, 0.7f), Quaternion.identity) as GameObject;
        }
    }
	
	
}
