using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class Interaction : MonoBehaviour
{
    public GameObject eventLauncherGO_start;
    private EventLauncher start_completed;

    private bool isStarted = false;

    public LayerMask interactableMask;
    public LayerMask targetableMask;
    public LayerMask groundMask;
    public GameObject viewfinder;
    public GameObject gravitySymbol;
    public GameObject handSymbol;
    public GameObject interactSymbol;
    public float interactableRayMaxDistance;
    public float controlRayMaxDistance;
    public float pickUpRayMaxDistance;
    //private SpriteRenderer viewfinder_renderer;

    // Start is called before the first frame update
    void Start()
    {
        //setto listener
        start_completed = eventLauncherGO_start.GetComponent<EventLauncher>();
        start_completed.EventToFire += OnEventReceived_start; //mi sottoscrivo all'evento --> divento LISTENER

        //viewfinder_renderer = viewfinder.GetComponent<SpriteRenderer>();
        gravitySymbol.SetActive(false);
        handSymbol.SetActive(false);
        viewfinder.SetActive(false);
        interactSymbol.SetActive(false);
    }

    void OnEventReceived_start(object sender, EventArgs args)
    {
        isStarted = true;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray viewRay = new Ray(this.transform.position, this.transform.forward);
        Debug.DrawRay(viewRay.origin,viewRay.direction,Color.blue);

        if (isStarted)
        {
            if (Physics.Raycast(viewRay, out hit, interactableRayMaxDistance, interactableMask))
            {
                gravitySymbol.SetActive(false);
                handSymbol.SetActive(false);
                viewfinder.SetActive(false);
                interactSymbol.SetActive(true);
                //viewfinder_renderer.color = Color.red;
            }
            else if (Physics.Raycast(viewRay, out hit, pickUpRayMaxDistance, targetableMask))
            {
                gravitySymbol.SetActive(false);
                handSymbol.SetActive(true);
                viewfinder.SetActive(false);
                interactSymbol.SetActive(false);
                //viewfinder_renderer.color = Color.blue;
            }
            else if (Physics.Raycast(viewRay, out hit, controlRayMaxDistance, groundMask))
            {
                gravitySymbol.SetActive(true);
                handSymbol.SetActive(false);
                viewfinder.SetActive(true);
                interactSymbol.SetActive(false);
                //viewfinder_renderer.color = Color.green;
            }
            else
            {
                gravitySymbol.SetActive(false);
                handSymbol.SetActive(false);
                viewfinder.SetActive(true);
                interactSymbol.SetActive(false);
                //viewfinder_renderer.color = Color.white;
            }
        }
    }
}
