using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(HeldObject))]
public class Gun : MonoBehaviour {

    //Variables
    public GameObject projectile;
    public float firePower;
    public Transform firePoint;

    public Valve.VR.EVRButtonId shootButton;

    HeldObject heldObject;

    public bool automatic;
    public float coolDownTime;
    float time;

	// Use this for initialization
	void Start () {
        heldObject = GetComponent<HeldObject>();
	}
	
	// Update is called once per frame
	void Update () {
        if (time > 0f)
        {
            time -= Time.deltaTime;
        }
        else
        {
            if (heldObject.parent != null && ((heldObject.parent.controller.GetPressDown(shootButton) && !automatic) || (heldObject.parent.controller.GetPress(shootButton) && automatic)))
            {
                time = coolDownTime;
                GameObject proj = Instantiate(projectile, firePoint.position, firePoint.rotation);
                proj.GetComponent<Rigidbody>().velocity = firePoint.forward * firePower;
            }
        }
	}
}
