using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    private Animator animator;

    private bool isPainted;
    public GameObject sheet;
    public Material paintMat;

    public LayerMask interactableMask;
    public Camera playerCamera;
    public float triggerDistance;

    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0)){
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, triggerDistance, interactableMask))
            {
                animator.SetTrigger("interactionTrigger");
                if (!isPainted)
                {
                    sheet.GetComponent<Renderer>().material = paintMat;
                    isPainted = true;
                }
                else
                {
                    isPainted = false;
                }
            }
        }
    }
}
