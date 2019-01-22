using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {


    //retrieves controller information so you dont have to type this out all the time
    public SteamVR_Controller.Device controller
    {
        get
        {
            return SteamVR_Controller.Input((int)GetComponent<SteamVR_TrackedObject>().index);
        }
    }
}
