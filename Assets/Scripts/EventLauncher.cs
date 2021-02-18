using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventLauncher : MonoBehaviour
{
    public event EventHandler EventToFire;

    public Material YESmat;
    public Material NOmat;

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
        if (obj.transform.localScale.x <= 0.20 && obj.transform.localScale.x >= 0.15)
        {
            if (obj.CompareTag("Room2obj"))
            {
                EventToFire(this, EventArgs.Empty);
                obj.GetComponent<Renderer>().material = YESmat;
            }
            else
            {
                obj.GetComponent<Renderer>().material = NOmat;
            }
        }
    }
}
