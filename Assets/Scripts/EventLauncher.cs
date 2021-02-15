using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventLauncher : MonoBehaviour
{
    public event EventHandler EventToFire;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        EventToFire(this, EventArgs.Empty);
    }

    void OnCollisionStay(Collision col)
    {
        GameObject obj = col.gameObject;
        if (obj.CompareTag("Room2obj"))
        {
            EventToFire(this, EventArgs.Empty);
            obj.GetComponent<Renderer>().material.color = Color.blue;
        }
        else
        {
            obj.GetComponent<Renderer>().material.color = Color.red;
        }
    }
}
