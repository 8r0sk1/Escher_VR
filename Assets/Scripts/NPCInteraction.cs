using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    private Animator animator;
    private MeshRenderer render;
    private Material baseMat;

    private bool toPaint = false;
    private bool isDrawing = false;
    public GameObject sheet;
    public Material paintMat;
    public Shader blendShader;
    public float blendSpeed = 1;
    private float alpha;

    public LayerMask interactableMask;
    public Camera playerCamera;
    public float triggerDistance;

    public AudioSource sound;


    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
        render = sheet.GetComponent<MeshRenderer>();

        render.material.shader = blendShader;

        baseMat = render.material;

        alpha = 0;

        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0)){
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, triggerDistance, interactableMask))
            {
                animator.SetTrigger("interactionTrigger");
                isDrawing = !isDrawing;
                toPaint = !toPaint;
                if (isDrawing) sound.Play();
                else sound.Stop();
            }
        }
        if (toPaint)
        {
            alpha = Mathf.Clamp(alpha + blendSpeed * Time.deltaTime, 0, 1);
            render.material.SetFloat("_Blend", alpha);
            if (alpha > 0.99f)
            {
                toPaint = false;
            }
        }
    }
}
