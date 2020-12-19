using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerTransform;
    private float cameraTilt = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //blocca il cursore al centro dello schermo
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //Debug.Log("x = " + mouseX);
        //Debug.Log("y = " + mouseY);

        cameraTilt -= mouseY; //aggiorno la variabile di rotazione della camera tilting
        cameraTilt = Mathf.Clamp(cameraTilt, -90f, 90f);

        playerTransform.Rotate(Vector3.up * mouseX); //ruoto il player attorno all'asse y (alto)
        transform.localRotation = Quaternion.Euler(cameraTilt, 0, 0);
    }
}
