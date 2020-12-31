using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("X and Z axis")]
    public CharacterController controller;
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
    public float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void moveY()
    {

        //Grouncheck e controllo jump
        isGrounded = Physics.CheckBox(groundCheck.position, Vector3.one * groundDistance,Quaternion.identity, groundMask);

        if (isGrounded && !Input.GetButtonDown("Jump") && !isJumping) velocity_y = 0; //controllo stato salto perchè non venga bloccato da collider circostanti
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity_y = Mathf.Sqrt(2f * -gravity * jumpHeight);
            //Debug.Log("velocity_up =" + velocity_y);

            //imposto stato salto
            isJumping = true;
        }

        if (isGrounded && !isJumping) velocity_y = 0;

        //controllo se ha finito il salto
        if(velocity_y <= 0)
        {
            isJumping = false;
        }

        //applico gravità
        velocity_y += gravity * Time.deltaTime;

        //effettuo il movimento lungo asse locale y
        controller.Move(transform.up.normalized * velocity_y * Time.deltaTime);
    }

    /// <summary>
    /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// </summary>
    void gravityControl()
    {

        if (Input.GetKeyDown(KeyCode.E) && !isChangingGravity){
            velocity_y = 0;
            isChangingGravity = true;
            rotationGravity = 90f;
        }

        if (isChangingGravity)
        {
            float rotationStep = rotationGravity * rotationSpeed * Time.deltaTime;
            transform.RotateAround(transform.position, Vector3.forward, rotationStep);
            rotationGravity -= rotationStep;

            if (rotationGravity <= 0.5f)
            {
                transform.RotateAround(transform.position, Vector3.forward, rotationGravity);
                isChangingGravity = false;
            }
        }
    }

    /// <summary>
    /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// </summary>

    void moveXZ()
    {
        float x = Input.GetAxis("Horizontal"); //asse a e d (in unity asse x)
        float z = Input.GetAxis("Vertical"); // asse w ed s (in unity asse z)

        Vector3 move = transform.right.normalized * x + transform.forward.normalized * z;
        controller.Move(move * speed * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {

        if (!isChangingGravity)
        {
            moveXZ();
            moveY();
        }

        gravityControl();
    }
}
