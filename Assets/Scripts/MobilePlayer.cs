using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.Networking;

public class MobilePlayer : NetworkBehaviour
{
    [SyncVar]
    public bool isRunning;
    [SyncVar]
    public float health = 100f;

    public const float maxHealth = 100f;

    
    GameObject blueSpawnPoint;


    public Animator animator;

    public GameObject localCamera;
    public GameObject joystickCanvas;
    public Joystick joystick;

    public GameObject heldObject;


    public float movementSpeed = 15.0f;
    public float cameraRotationSpeed = 2f;
    public float cameraStartHeight = 1f;
    public float cameraStartDistance = -2f;


    private Vector3 cameraOffsetX;
    private Vector3 cameraOffsetY;

    private void Start()
    {
        blueSpawnPoint = GameObject.Find("BlueSpawn");
    }

    public override void OnStartLocalPlayer(){
        Debug.Log("ZZZ Added mobile player.");
        GameObject.Find("MobileMenuCamera").SetActive(false);
        
        localCamera.SetActive(true);

        #if !UNITY_EDITOR && (UNITY_IOS || UNITY_ANDROID)
                joystickCanvas.SetActive(true);
                joystick.gameObject.SetActive(true);
        #endif

        // Rotate camera to default height and distance
        cameraOffsetX = new Vector3(0, cameraStartHeight, cameraStartDistance);
        cameraOffsetY = new Vector3(0, 0, cameraStartDistance);
        RotateCamera(Vector2.zero);
    }

    public override void OnStartClient(){
    }

    /// <summary>
    /// Moves the player in the given direction.
    /// Note: This is a [Command] meaning the client tells the server to take this action.
    /// </summary>
    /// <param name="movementDirection"></param>
    void CmdMove(Vector3 movementDirection)
    {
        gameObject.transform.position += movementDirection * movementSpeed * Time.deltaTime;
        gameObject.transform.LookAt(gameObject.transform.position + movementDirection);
    }

    //
    /// <summary>
    ///  RotateCamera can be used to rotate the mobile player's camera relative to the player
    /// </summary>
    /// <param name="eulerAngles">A Vector2 where the x component is the amount the camera should be rotated in the xz plane
    /// and the y component is the amount the camera should be rotated in the yz plane</param>
    void RotateCamera(Vector2 eulerAngles)
    {
        if (localCamera.active)
        {
            cameraOffsetX = Quaternion.AngleAxis(eulerAngles.x * cameraRotationSpeed, Vector3.up) * cameraOffsetX;
            cameraOffsetY = Quaternion.AngleAxis(eulerAngles.y * cameraRotationSpeed, Vector3.right) * cameraOffsetY;
            localCamera.transform.position = transform.position + cameraOffsetX + cameraOffsetY;
            localCamera.transform.LookAt(transform.position);
        }
        else
        {
            Debug.Log("ZZZ camera is not active");
        }


    }

    [Command]
    void CmdSetRunning(bool isRunningIn){
        isRunning = isRunningIn;
    }

   

    public void TakeDamage(float amount)
    {
        Debug.LogFormat("ZZZ MobilePlayer take damage {0} to {1}. isServer {2}, isClient {3}, isLocalPlayer {4}, hasAuthority {5}",
            amount.ToString(), gameObject.name.ToString(),isServer,isClient,isLocalPlayer,hasAuthority);
        if (!isServer)
            return;

        health -= amount;
        if (health <= 0)
        {
            health = maxHealth;

            // called on the Server, but invoked on the Clients
            Respawn();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("YYY OnCollisionEnter");
        ContactPoint contact = collision.contacts[0];
        Debug.LogFormat("ZZZ oncollisionenter {0}",collision.gameObject.name);
        Destructable destructable = collision.gameObject.GetComponent<Destructable>();
        Holdable holdable = collision.gameObject.GetComponent<Holdable>();
        if ( holdable != null){
            holdable.PickUp(gameObject);
            heldObject = holdable.gameObject;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Golden Shower");
        if (heldObject != null){
            heldObject.GetComponent<Holdable>().ResetPosition();
            heldObject = null;
        }
    }




    
    void Respawn()
    {
        transform.position = blueSpawnPoint.transform.position;
    }

    void Update()
    {

        if (isLocalPlayer){
            if (Mathf.Abs(Input.GetAxis("Vertical")) > 0.0f || Mathf.Abs(Input.GetAxis("Horizontal")) > 0.0f
                || Mathf.Abs(joystick.Horizontal) > 0.0f || Mathf.Abs(joystick.Vertical) > 0.0f)
            {
                CmdSetRunning(true);
            }else{
                CmdSetRunning(false);
            }

#if UNITY_EDITOR || UNITY_STANDALONE_OSX
            CmdMove((Input.GetAxis("Vertical") * Vector3.forward) + (Input.GetAxis("Horizontal") * Vector3.right));
            RotateCamera(new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")));
#elif UNITY_IOS || UNITY_ANDROID
            CmdMove(new Vector3(joystick.Horizontal,0,joystick.Vertical));
#endif
        }
        animator.SetBool("IsRunning", isRunning);
    }
}

