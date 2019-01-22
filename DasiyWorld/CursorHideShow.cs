using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorHideShow : MonoBehaviour {

    bool isLocked = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            isLocked = false;
        }

        if (isLocked == false)
        {
            SetCursorLock();
        }

	}

    void SetCursorLock()
    {

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    
    }
}
