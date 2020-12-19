using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 10;
    public float jumpHeight = 2;
    private float gravity = -9.81f;
    private Vector3 velocity = new Vector3(0,0,0);

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position,groundDistance,groundMask);

        float x = Input.GetAxis("Horizontal"); //asse a e d (in unity asse x)
        float z = Input.GetAxis("Vertical"); // asse w ed s (in unity asse z)

        Vector3 move = transform.right.normalized * x + transform.forward.normalized * z;
        controller.Move(move * speed * Time.deltaTime);

        if (isGrounded && velocity.y < 0) velocity.y = -2f;
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(2f * -gravity * jumpHeight); 
            Debug.Log("velocity.y =" + velocity.y);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
