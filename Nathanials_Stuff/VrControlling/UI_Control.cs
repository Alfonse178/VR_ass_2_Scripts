using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI_Control : MonoBehaviour {

    //Reference to the text
    public Text titleText;
    public Text controllerPositionVector3;
    public Text controllerVelocityVector3;
    public Text controllerAngularVector3;

    private Controller controller;

    // Use this for initialization
    void Start () {

        controller = GetComponent<Controller>();
        titleText.text = "Controller Information:";
    }
	
	// Update is called once per frame
	void Update () {

        controllerPositionVector3.text = "Position XYZ : " + controller.controller.transform.pos;
        controllerVelocityVector3.text = "Velocity XYZ : " + controller.controller.velocity.ToString();
        controllerAngularVector3.text = "Angular Velocity : " + controller.controller.angularVelocity.ToString();
    }
}
