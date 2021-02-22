using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventLauncher : MonoBehaviour
{
    public event EventHandler EventToFire;

    public Material NOmat;

    public GameObject room2obj;

    private bool isRoom2;

    // Start is called before the first frame update
    void Start()
    {
        room2obj.GetComponent<MeshRenderer>().enabled = false;
    }

    private void OnTriggerEnter(Collider col)
    {
        isRoom2 = this.gameObject.name.Equals("EventLauncher_Room2");
        if (!isRoom2)
        {
            EventToFire(this, EventArgs.Empty);
        }
    }

    private void OnTriggerStay(Collider col)
    {
        if(isRoom2)
        {
            GameObject obj = col.gameObject;
            //Debug.Log(obj.transform.localScale);
            if (obj.transform.localScale.x >= 0.15 && obj.transform.localScale.x <= 0.30)
            //if (true)
            {
                if (obj.CompareTag("Room2obj"))
                {
                    EventToFire(this, EventArgs.Empty);
                    UnityEngine.Object.Destroy(obj);
                    room2obj.GetComponent<MeshRenderer>().enabled = true;
                }
                else
                {
                    obj.GetComponent<Renderer>().material = NOmat;
                }
            }
        }
    }

}
