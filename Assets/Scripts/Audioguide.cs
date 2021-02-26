using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audioguide : MonoBehaviour
{
    public LayerMask audioguideMask;
    public Camera playerCamera;
    public float triggerDistance = 10;
    AudioSource audioguide = null;

    void Start()
    {
        audioguide = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit hit;
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward,out hit, triggerDistance, audioguideMask))
            {
                Debug.Log(hit.collider);
                Debug.DrawLine(hit.point, playerCamera.transform.position);
                audioguide.Play();
            }
        }
    }
}
