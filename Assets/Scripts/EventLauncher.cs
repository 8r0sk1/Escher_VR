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

    void OnTriggerEnter()
    {
        EventToFire(this, EventArgs.Empty);
    }
}
