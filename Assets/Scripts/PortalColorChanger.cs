using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalColorChanger : MonoBehaviour
{
    public GameObject otherPortal;

    private Renderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        renderer = otherPortal.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void OnTriggerEnter()
    {
        renderer.material.color = Color.blue;
    }
}
