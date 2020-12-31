using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public LayerMask interactableMask;
    public LayerMask targetableMask;
    public GameObject viewfinder;
    public float interactableRayMaxDistance;
    private SpriteRenderer viewfinder_renderer;

    // Start is called before the first frame update
    void Start()
    {
        viewfinder_renderer = viewfinder.GetComponent < SpriteRenderer >();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray viewRay = new Ray(this.transform.position, this.transform.forward);
        Debug.DrawRay(viewRay.origin,viewRay.direction,Color.blue);

        if (Physics.Raycast(viewRay, out hit, interactableRayMaxDistance, interactableMask))
            viewfinder_renderer.color = Color.red;
        else if (Physics.Raycast(viewRay, out hit, Mathf.Infinity, targetableMask))
            viewfinder_renderer.color = Color.blue;
        else viewfinder_renderer.color = Color.white;

    }
}
