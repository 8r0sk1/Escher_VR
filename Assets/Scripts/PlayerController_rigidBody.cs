using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_rigidBody : MonoBehaviour
{
    [Header("X and Z axis")]
    public Rigidbody controller;
    public float speed = 10;

    [Header("Y axis")]
    public float jumpHeight = 2;
    private float gravity = -9.81f;
    private float velocity_y = 0f;
    public LayerMask groundMask;
    public Transform groundCheck;
    public float groundDistance = 0.4f;

    private bool isGrounded;
    private bool isJumping = false;

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
    }

    void move()
    {
        Vector3 move; //vettore di spostamento

        ////XZ AXIS////
        float x = Input.GetAxis("Horizontal"); //asse a e d (in unity asse x)
        float z = Input.GetAxis("Vertical"); // asse w ed s (in unity asse z)

        ////Y AXIS////

        //applico gravità
        controller.AddForce(transform.up * gravity, ForceMode.Force);

        //Grouncheck e controllo jump
        isGrounded = Physics.CheckBox(groundCheck.position, Vector3.one * groundDistance, Quaternion.identity, groundMask);

        //if (isGrounded && !Input.GetButtonDown("Jump") && !isJumping) velocity_y = 0; //controllo stato salto perchè non venga bloccato da collider circostanti
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            //velocity_y = Mathf.Sqrt(2f * -gravity * jumpHeight);
            controller.AddForce(transform.up * Mathf.Sqrt(2f * -gravity * jumpHeight), ForceMode.VelocityChange);
            Debug.Log("velocity change to "+ Mathf.Sqrt(2f * -gravity * jumpHeight));

            //imposto stato salto
            isJumping = true;
        }

        Debug.Log("actual velocity " + controller.velocity.y);

        //controllo se ha finito il salto
        /*if (velocity_y <= 0)
        {
            isJumping = false;
        }*/

        //applico gravità
        //velocity_y += gravity * Time.fixedDeltaTime;

        //calcolo il vettore totale di velocità
        //move = transform.up.normalized * velocity_y * Time.fixedDeltaTime + (transform.right.normalized * x + transform.forward.normalized * z) * speed / 10;
        move = (transform.right.normalized * x + transform.forward.normalized * z) * speed / 10;

        //effettuo il movimento lungo asse locale y
        controller.MovePosition(controller.transform.position + move);
    }

    void gravityControl()
    {

        if (Input.GetKeyUp(KeyCode.Mouse1) && !isChangingGravity)
        {
            //lancio un raggio che incide con il "ground" layer
            RaycastHit hit;
            Ray gRay = new Ray(playerCamera.transform.position,playerCamera.transform.forward);

            if (Physics.Raycast(gRay, out hit, controlRayMaxDistance, groundMask))
            {
                velocity_y = 0;
                isChangingGravity = true;

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
            }
        }
    }

    /// <summary>
    /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// </summary>

    void FixedUpdate()
    {
        controller.AddForce(Vector3.zero, ForceMode.Acceleration);
        if (!isChangingGravity)
        {
            move();
        }
    }

    // Update is called once per frame
    void Update()
    {
        gravityControl();
    }
}
