using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalDeactivation : MonoBehaviour
{
    public GameObject portal;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void OnTriggerEnter()
    {
        portal.SetActive(false);
    }
}
