
using UnityEngine;
using System.Collections;
using Valve.VR;

// ############### MADE BY ALFIE SARGENT ###############

//A script that is used to simulate gravity around a small object. Applied to the player character
//it enables the user to move around the "planets" within the scene; essentially sticking them to 
//the spherical object. The force is applied in the GravityAttractor. 

//it NEEDS the gravitybody for it to actually be affected.
//particularly because the in-built gravity has been disabled! 

[RequireComponent(typeof(GravityBody))]
public class FirstPersonController : MonoBehaviour
{
    //basic steam VR setup so that the Vive can be used
    //the inputs map to the controllers by default for movement and jumping.
    private SteamVR_TrackedObject trackedObj;
    private Vector2 axis = Vector2.zero;
    private float speed;
    public SteamVR_Controller.Device mDevice;

    void Start()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    // So apparently we cant use the in-built first person controller...great
    //setting float values for mouse, walk speed and jumping force.
    //set to public floats to edit between planets within unity editor.

    public float mouseSensitivityX = 1;
    public float mouseSensitivityY = 1;
    public float walkSpeed = 8;
    public float jumpForce = 200;
    public LayerMask Mask;

    // System vars

    bool gnd;
    Vector3 moveNo;
    Vector3 smoothvelocity;
    float verticalLookRotation;
    Transform cameraTransform;
    Rigidbody rigidbody;


    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cameraTransform = Camera.main.transform;
        rigidbody = GetComponent<Rigidbody>();

    }

    void Update()
    {
        // ########### CODE FOR VIEWING ###########

        // Look rotation: this does also work for the vive controllers
        //rotation in the mouse for the x & y values clamped at -60 to 60 so that the user cant
        // look beyond what would be natural. The user can't look beyond their feet/over their head.

        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * mouseSensitivityX);
        verticalLookRotation += Input.GetAxis("Mouse Y") * mouseSensitivityY;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -60, 60);
        cameraTransform.localEulerAngles = Vector3.left * verticalLookRotation;

        // ########### CODE FOR MOVEMENT ###########

        // Calculate movement: just using the x and y axis to get the movement

        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        //By default this is the WASD keys
        //just getting the x and y inputs, normalized so that it is the same length.
        //stops people from running faster if both W & A was pressed, remains one length.
        //smoothing of frames is also done to ensure nice movement. Only using a smaller value so
        //the movement isnt too slippery (could change for ice planet?)

        Vector3 moveDirection = new Vector3(inputX, 0, inputY).normalized;
        Vector3 targetMovement = moveDirection * walkSpeed;
        moveNo = Vector3.SmoothDamp(moveNo, targetMovement, ref smoothvelocity, .15f);

        // ########### CODE FOR JUMPING ###########

        //simple if statement, if the jump button is pressed, and if they are grounded, apply the jump force up
        //important to note that this only works when grounded, no double jumping!
        //jump is assigned to space bar in unity by default.

        if (Input.GetButtonDown("Jump"))
        {
            if (gnd)
            {
                rigidbody.AddForce(transform.up * jumpForce);
            }
        }

        // using raycasting to detect if the player is able to jump or not.
        //important to note that a capsule is being used for the player which is 2 high. Therefore, the ray
        //that is being cast must be cast 1 unit further away. An extra .1 has been added for slope 
        //defficencies. Checking what mask is being used as well, only want it on the surface.

        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hitconfirm;

        if (Physics.Raycast(ray, out hitconfirm, 1 + .1f, Mask))
        {
            gnd = true;
        }
        else
        {
            gnd = false;
        }

    }

    void FixedUpdate()
    {
        // Apply movement to rigidbody. VERY IMPORTANT THAT THE MOVEMENT UPDATES ON ITS OWN AXIS
        //Changes the update of movement from world axis to a local axis when it rotates as it moves
        //around the planet

        Vector3 localMove = transform.TransformDirection(moveNo) * Time.fixedDeltaTime;
        rigidbody.MovePosition(rigidbody.position + localMove);
    }
}