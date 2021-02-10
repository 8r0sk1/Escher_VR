using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_rigidBody : MonoBehaviour
{
    [Header("X and Z axis")]
    public Rigidbody controller;
    public float speed = 1;
    //public float scaleSpeed = 1;
    private float speedFactor = 10;

    [Header("Y axis")]
    public float jumpHeight = 2;
    private float gravity = -9.81f;
    private float velocity_y;
    private bool isJumping;
    public LayerMask groundMask;
    public LayerMask stairMask;
    public Transform groundCheck;
    public float groundDistance = 0.4f;

    [Header("StairsControl")]
    //private Collider col;
    public float stairDistance;
    public float stairAngle;
    private bool isOnStair;
    private bool isGrounded;
    Vector3 stairMove;

    [Header("Gravity")]
    private bool isChangingGravity = false;

    private float rotationGravity = 0f;
    Vector3 rotationAxis;

    public float controlRayMaxDistance;
    //private float rotationGravity_x = 0f;
    //private float rotationGravity_z = 0f;

    public float rotationSpeed;

    [Header("Camera")]
    private Camera playerCamera;

    // Start is called before the first frame update
    void Start()
    {
        playerCamera = this.GetComponentInChildren<Camera>();
        //col = this.GetComponentInChildren<Collider>();
    }

    void jumpControl()
    {
        //Grouncheck e controllo jump
        isGrounded = Physics.CheckBox(groundCheck.position, Vector3.one * groundDistance, Quaternion.identity, groundMask) || isOnStair; //controllo se è anche sulle scale (coem fosse grounded)

        if (isGrounded && !Input.GetButton("Jump") && !isJumping) velocity_y = 0; //controllo stato salto perchè non venga bloccato da collider circostanti
        if (isGrounded && Input.GetButton("Jump"))
        {
            velocity_y = Mathf.Sqrt(2f * -gravity * jumpHeight);

            //imposto stato salto
            isJumping = true;
        }

        if (isGrounded && !isJumping) velocity_y = 0;

        //controllo se ha finito il salto
        if (velocity_y <= 0)
        {
            isJumping = false;
        }

        //if (isOnStair) isGrounded = true;
    }

    void stairControl()
    {
        RaycastHit hit;
        Ray stairRay = new Ray(groundCheck.transform.position, -groundCheck.transform.up);

        if (Physics.Raycast(stairRay, out hit, stairDistance, stairMask))
        {
            float dotResult = Vector3.Dot(hit.normal, groundCheck.transform.up);
            Debug.Log("dotResult: " + dotResult + "\n");

            float angle = Mathf.Acos(dotResult);
            Debug.Log("angle: " + angle + "\n");

            if (angle < 90-stairAngle)
            {
            /*    if (!isOnStair)
                {
                    Vector3 stairVector = Quaternion.AngleAxis(angle, transform.up) * transform.forward;
                    stairMove = transform.up * scaleSpeed / speedFactor * dotResult;
                }
                else stairMove = Vector3.zero;*/
                isOnStair = true;
                //Debug.Log("isOnStair\n");
            }
        }
        else
        {
            //stairMove = Vector3.zero;
            isOnStair = false;
            //Debug.Log("isOnNotStair\n");
        }
    }

    void move()
    {
        Vector3 moveXZ;

        ////XZ AXIS////
        float x = Input.GetAxis("Horizontal"); //asse a e d (in unity asse x)
        float z = Input.GetAxis("Vertical"); // asse w ed s (in unity asse z)

        moveXZ = (transform.right * x + transform.forward * z) * speed/speedFactor;

        ////Y AXIS////
        jumpControl();
        stairControl();

        if (isOnStair && velocity_y < 0) velocity_y = 0; //controllo se è sulle scale così da evitare scivolamento

        //applico gravità
        if (!isOnStair)velocity_y += gravity * Time.deltaTime;
        Vector3 moveY = transform.up * velocity_y * Time.deltaTime + stairMove;

        //calcolo vettore di spostamento totale
        Vector3 move = moveXZ + moveY;

        //calcolo nuova posizione
        Vector3 new_pos = transform.position + move;

        //effettuo il movimento lungo asse locale y
        controller.MovePosition(new_pos);
    }

    void FixedUpdate()
    {
        if (!isChangingGravity)
        {
            move();
        }
    }


    void gravityControl()
    {

        if (Input.GetKeyUp(KeyCode.Mouse1) && !isChangingGravity)
        {
            //lancio un raggio che incide con il "ground" layer
            RaycastHit hit;
            Ray gRay = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

            if (Physics.Raycast(gRay, out hit, controlRayMaxDistance, groundMask))
            {
                isChangingGravity = true;
                controller.freezeRotation = false;
                velocity_y = 0;

                rotationAxis = Vector3.Cross(this.transform.up, hit.normal);
                rotationGravity = Vector3.SignedAngle(this.transform.up, hit.normal, rotationAxis);

                Debug.DrawRay(this.transform.position, rotationAxis, Color.green, 5);
                Debug.DrawRay(hit.point, hit.normal, Color.green, 5);
            }
        }

        if (isChangingGravity)
        {
            float rotationStep = rotationGravity * rotationSpeed * Time.deltaTime;
            transform.RotateAround(transform.position, rotationAxis, rotationStep);
            rotationGravity -= rotationStep;

            Debug.Log("step = " + rotationStep);

            if (Mathf.Abs(rotationGravity) <= 0.5f)
            {
                transform.RotateAround(transform.position, rotationAxis, rotationGravity);
                isChangingGravity = false;
                controller.freezeRotation = true;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        gravityControl();
    }
}
