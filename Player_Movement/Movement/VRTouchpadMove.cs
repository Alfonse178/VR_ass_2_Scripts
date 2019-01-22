using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRTouchpadMove : MonoBehaviour
{

    [SerializeField]
    private Transform rig;
    [SerializeField]
    private Transform head; //the headset will be moving so the camera will move with it
    [SerializeField]
    private float walkingSpeed; //value for walking speed
    [SerializeField]
    private float sprintSpeed; //value for sprint speed

    private Valve.VR.EVRButtonId touchpad = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad; //for basic movement
    private Valve.VR.EVRButtonId grip = Valve.VR.EVRButtonId.k_EButton_Grip; //for the sprint

    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    private SteamVR_TrackedObject trackedObj;

    private Vector2 axis = Vector2.zero;
    private float speed;

    void Start()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }


    //used for the actual movement, determines when something is presses, what direction the headset and touchpad axis is in
    void Update()
    {
        //initial check to see if the controller is actually active
        if (controller == null)
        {
            Debug.Log("Controller not initialized");
            return;
        }

        var device = SteamVR_Controller.Input((int)trackedObj.index);

        if (controller.GetTouch(touchpad))
        {
            //if both the touchpad and grip button are pressed, sprint speed s used.
            if (controller.GetPress(grip))
            {
                speed = sprintSpeed;
            }
            else
            //else the normal walking speed is used
            {
                speed = walkingSpeed;
            }

            //getting the direction that the touchpad is being pressed
            axis = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0);


            //so if the touch pad is not equal to zero, the player wants to move.
            if (rig != null)
            {
                //using both the head direction and the axis direction to determine the movement
                rig.position += (head.right * axis.x + head.forward * axis.y) * speed * Time.deltaTime;
                rig.position = new Vector3(rig.position.x, 0, rig.position.z);
            }

        }

    }


}