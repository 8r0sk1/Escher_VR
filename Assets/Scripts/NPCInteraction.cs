using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    private Animator animator;
    private Renderer renderer;
    private Material baseMat;

    private bool toPaint = false;
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
        renderer = sheet.GetComponent<Renderer>();

        renderer.material.shader = blendShader;

        baseMat = renderer.material;

        alpha = 0;

        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0)){
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, triggerDistance, interactableMask))
            {
                animator.SetTrigger("interactionTrigger");
                sound.Play();
                toPaint = !toPaint;
            }
        }
        if (toPaint)
        {
            alpha = Mathf.Clamp(alpha + blendSpeed * Time.deltaTime, 0, 1);
            renderer.material.SetFloat("_Blend", alpha);
            if (alpha > 0.99f)
            {
                toPaint = false;
                sound.Stop();
            }
        }
    }
}
