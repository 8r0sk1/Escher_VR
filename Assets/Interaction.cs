using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public string interactableTag;
    public GameObject viewfinder;
    public float rayMaxDistance;
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

        if (Physics.Raycast(viewRay, out hit, rayMaxDistance) && hit.transform.CompareTag(interactableTag))
            viewfinder_renderer.color = Color.red;
        else viewfinder_renderer.color = Color.white;

    }
}
