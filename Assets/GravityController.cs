using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour
{
    public CharacterController controller;
    public float jumpHeight = 2;
    private float gravity = -9.81f;
    private Vector3 velocity = Vector3.zero;
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
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && !Input.GetButtonDown("Jump")) velocity.y = 0;
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(2f * -gravity * jumpHeight);
            Debug.Log("velocity.y =" + velocity.y);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Keypad1)) //cambio gravità
        {
            velocity.y = 0;
            gravity = -gravity;
            transform.Rotate(new Vector3(180, 0, 0));
        }
    }
}
