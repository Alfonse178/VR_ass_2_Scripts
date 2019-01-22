using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour {

    public float jumpForce = 220;
    public SteamVR_TrackedObject mtrackedobj = null;
    public SteamVR_Controller.Device mdevice;


    // Use this for initialization
    void Start () {
        mtrackedobj = GetComponent<SteamVR_TrackedObject>();
	}

    bool grounded;
    Rigidbody rigidbody;
    public LayerMask groundedMask;
    // Update is called once per frame
    void Update () {

        mdevice = SteamVR_Controller.Input((int)mtrackedobj.index);

            // Jump
       if (mdevice.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            if (grounded)
            {
                rigidbody.AddForce(transform.up* jumpForce);
            }
        }

        // Grounded check
        Ray ray = new Ray(transform.position, -transform.up);
RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1 + .1f, groundedMask))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }

}
