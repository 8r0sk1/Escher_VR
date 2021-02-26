using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioGuideInteraction : MonoBehaviour
{
    private RaycastHit hit;
    private AudioSource source;
    public LayerMask audioguideMask;
    public float triggerDistance = 5;

    public GameObject UIstart;
    public GameObject UIstop;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, triggerDistance, audioguideMask))
        {
            source = hit.collider.GetComponent<AudioSource>();
            if (source != null)
            {
                if (source.isPlaying)
                {
                    UIstop.SetActive(true);
                    UIstart.SetActive(false);
                }
                else
                {
                    UIstop.SetActive(false);
                    UIstart.SetActive(true);
                }
            }

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                //Debug.Log(hit.collider);
                //Debug.DrawLine(hit.point, this.transform.position);
                if (!source.isPlaying) source.Play();
                else source.Stop();
            }
        }
        else
        {
            UIstop.SetActive(false);
            UIstart.SetActive(false);
        }
    }
}