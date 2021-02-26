using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audioguide : MonoBehaviour
{
    public LayerMask audioguideMask;
    public Camera playerCamera;
    public float triggerDistance;
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
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, triggerDistance, audioguideMask))
            {
                audioguide.Play();
            }
        }
    }
}
