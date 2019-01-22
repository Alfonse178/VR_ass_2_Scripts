using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Controller))]
public class Hand : MonoBehaviour {

    //Reference to the object to be held
    GameObject heldObject;
    //Reference to the controller doing the holding
    Controller controller;

    Rigidbody simulator;

    public Valve.VR.EVRButtonId pickUpButton;
    public Valve.VR.EVRButtonId dropButton;

    


    // Use this for initialization
    void Start () {
        //create the simulator reference as a new object of a rigidbody
        simulator = new GameObject().AddComponent<Rigidbody>();
        simulator.name = "simulator";
        //set the parent of the simulator object to be the controller
        simulator.transform.parent = transform.parent;

        //fill the Controller reference with the correct controller information
        controller = GetComponent<Controller>();
    }
	
	// Update is called once per frame
	void Update () {
        //if there is a held object (TRUE)
        if (heldObject)
        {
            //calculate the simulator velocity by taking the position of the controller - the position of the simulator object
            //the 50f value is just a general sweet spot 
            simulator.velocity = (transform.position - simulator.position) * 50f;
            

            //check if the trigger of the controller is released (UP) and make it so that the held object is no longer attached to the controller
            if ((controller.controller.GetPressUp(pickUpButton) && heldObject.GetComponent<HeldObject>().dropOnRelease) || (controller.controller.GetPressDown(dropButton) && !heldObject.GetComponent<HeldObject>().dropOnRelease))
            {
                //stop the held object from adopting the transformation of the controller
                heldObject.transform.parent = null;
                //this pretty much turns the gravity property back on, for the held object
                heldObject.GetComponent<Rigidbody>().isKinematic = false;

                tossObject(heldObject.GetComponent<Rigidbody>());
                                        
                //Turn off the model rendering from Valve's code that is letting go of the object
                SetControllerVisible(heldObject.GetComponent<HeldObject>().parent, true);
                //Disconnect the held object from being a child of the parent controller object
                heldObject.GetComponent<HeldObject>().parent = null;
                //clear the reference of the held object
                heldObject = null;

            }
        }
        //if there ISNT a held object, check if the trigger is being held pressed
        //the naming conventions for the buttons are not convenient but it will do
        else
        {
            //This is long winded for checking if the referenced controller has it's Trigger button pressed down
            if (controller.controller.GetPressDown(pickUpButton))
            {
                //array of colliders that are in a small radius of the controller
                Collider[] cols = Physics.OverlapSphere(transform.position, 0.1f);

                //run through each of the colliders in the array to check if they are ready to be a held object
                foreach (Collider col in cols)
                {
                    //check if there is NO held object to begin with to BE a held object
                    //check if the object IS a held object
                    //check if its not being held by anything else
                    if (heldObject == null && col.GetComponent<HeldObject>() && col.GetComponent<HeldObject>().parent == null)
                    {
                        //set reference to the object to be held from the collider array by radial area of the controller
                        heldObject = col.gameObject;
                        //make the held object set so that it follows the controller around
                        heldObject.transform.parent = transform;
                        //Set the held object's position so that it is referenced to the controller at vector3 all zeros (0, 0, 0)
                        //but add an offset vector so that the object (GUN) may be in line more naturally with the controller
                        heldObject.transform.localPosition = Vector3.zero + new Vector3(-0.07f, 0f, 0.06f);
                        //Set the held object's rotation vector is that in quaternion form of the identity matrix matching the controller's rotation
                        //Set an offset for rotation in the same principle as positional vector but multiply by the inverse quaternion (invert also the euler ints)
                        heldObject.transform.localRotation = Quaternion.identity;
                        //This turns "off" the gravity effect on the held object
                        heldObject.GetComponent<Rigidbody>().isKinematic = true;
                        //This sets the object being held as a child of the controller
                        heldObject.GetComponent<HeldObject>().parent = controller;
                        //Turn off the valve controller model renderer that is doing the holding
                        SetControllerVisible(heldObject.GetComponent<HeldObject>().parent, false);

                    }
                }
            }
        }
	}

    void SetControllerVisible(Controller controller, bool visible)
    {
        Debug.Log("HELLO");
        foreach (SteamVR_RenderModel model in controller.GetComponentsInChildren<SteamVR_RenderModel>())
        {
            foreach (var child in model.GetComponentsInChildren<MeshRenderer>())
                child.enabled = visible;
        }
    }

    void tossObject(Rigidbody rigidbody)
    {
        rigidbody.velocity = controller.controller.velocity;
        rigidbody.angularVelocity = controller.controller.angularVelocity;
    }

    public string GetVelocityAsString()
    {
        return controller.controller.velocity.ToString();
    }

}






