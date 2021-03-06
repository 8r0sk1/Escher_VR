﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalingMechanic : MonoBehaviour
{
    [Header("Components")]
    public Transform target;            // The target object we picked up for scaling
    private Collider col;
    MeshRenderer renderer;

    [Header("Parameters")]
    public LayerMask targetMask;        // The layer mask used to hit only potential targets with a raycast
    public LayerMask ignoreTargetMask;  // The layer mask used to ignore the player and target objects while raycasting
    public float offsetFactor;          // The offset amount for positioning the object so it doesn't clip into walls

    float originalDistance;             // The original distance between the player camera and the target
    float originalScale;                // The original scale of the target objects prior to being resized
    Vector3 targetScale;                // The scale we want our object to be set to each frame

    //per controllare adjust di oggetti scalati
    bool isAdjusted = false;
    Vector3 adjust_position = Vector3.zero;
    Vector3 old_hitPoint;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        HandleInput();
        ResizeTarget();
    }

    /*
    void FixedUpdate()
    {
        ResizeTarget();
    } */

    void HandleInput()
    {
        // Check for left mouse click
        if (Input.GetMouseButtonDown(0))
        {
            // If we do not currently have a target
            if (target == null)
            {
                // Fire a raycast with the layer mask that only hits potential targets
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, targetMask))
                {
                    // Set our target variable to be the Transform object we hit with our raycast
                    target = hit.transform;
                    renderer = target.GetComponent<MeshRenderer>();
                    col = target.GetComponent<Collider>();

                    // Disable physics for the object
                    target.GetComponent<Rigidbody>().isKinematic = true;
                    //target.GetComponent<Rigidbody>().useGravity = false;
                    //target.GetComponent<Rigidbody>().freezeRotation = true;

                    // Calculate the distance between the camera and the object
                    originalDistance = Vector3.Distance(transform.position, target.position);

                    // Save the original scale of the object into our originalScale Vector3 variabble
                    originalScale = target.localScale.x;

                    // Set our target scale to be the same as the original for the time being
                    targetScale = target.localScale;
                }
            }
            // If we DO have a target
            else
            {
                // Reactivate physics for the target object
                target.GetComponent<Rigidbody>().isKinematic = false;
                //target.GetComponent<Rigidbody>().useGravity = true;
                //target.GetComponent<Rigidbody>().freezeRotation = false;

                // Set our target variable to null
                target = null;
            }
        }
    }

    void ResizeTarget()
    {
        // If our target is null
        if (target == null)
        {
            // Return from this method, nothing to do here
            return;
        }

        // Cast a ray forward from the camera position, ignore the layer that is used to acquire targets
        // so we don't hit the attached target with our ray
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, ignoreTargetMask))
        {

            // Set the new position of the target by getting the hit point and moving it back a bit
            // depending on the scale and offset factor

            //target.position = (hit.point - transform.forward * offsetFactor * targetScale.x);
            //target.GetComponent<Rigidbody>().MovePosition(hit.point + hit.normal * offsetFactor * targetScale.x);

            //target.position = (hit.point + hit.normal * offsetFactor * targetScale.x);
            //target.GetComponent<Rigidbody>().MovePosition(new_pos);

            //CONTROLLO SULLA COLLISIONE TRA OGGETTO E ALTRO

            if (!isAdjusted) //controllo se la posizione è già stata aggiornata
            {
                adjust_position = Vector3.zero;
                RaycastHit obj_hit;

                //se c'è da aggiustare la posizione ajust_position != 0;
                if (Physics.Raycast(target.position, target.right, out obj_hit, col.bounds.extents.x, ignoreTargetMask))
                {
                    adjust_position.x = col.bounds.size.x - (target.position - obj_hit.point).magnitude;
                    Debug.DrawLine(target.position, obj_hit.point, Color.yellow);
                }
                if (Physics.Raycast(target.position, target.up, out obj_hit, col.bounds.extents.y, ignoreTargetMask))
                {
                    adjust_position.y = col.bounds.size.y - (target.position - obj_hit.point).magnitude;
                    Debug.DrawLine(target.position, obj_hit.point, Color.yellow);
                }
                if (Physics.Raycast(target.position, target.forward, out obj_hit, col.bounds.extents.z, ignoreTargetMask))
                {
                    adjust_position.z = col.bounds.size.z - (target.position - obj_hit.point).magnitude;
                    Debug.DrawLine(target.position, obj_hit.point, Color.yellow);
                }
                if (Physics.Raycast(target.position, -target.right, out obj_hit, col.bounds.extents.x, ignoreTargetMask))
                {
                    adjust_position.x = col.bounds.size.x - (target.position - obj_hit.point).magnitude;
                    Debug.DrawLine(target.position, obj_hit.point, Color.yellow);
                }
                if (Physics.Raycast(target.position, -target.up, out obj_hit, col.bounds.extents.y, ignoreTargetMask))
                {
                    adjust_position.y = col.bounds.size.y - (target.position - obj_hit.point).magnitude;
                    Debug.DrawLine(target.position, obj_hit.point, Color.yellow);
                }
                if (Physics.Raycast(target.position, -target.forward, out obj_hit, col.bounds.extents.z, ignoreTargetMask))
                {
                    adjust_position.z = col.bounds.size.z - (target.position - obj_hit.point).magnitude;
                    Debug.DrawLine(target.position, obj_hit.point, Color.yellow);
                }

                //aggiusto la posizione
                target.position = (hit.point - transform.forward * offsetFactor * targetScale.x) - adjust_position;
                old_hitPoint = hit.point; //memorizzo il punto di incidenza
                isAdjusted = true;
                //renderer.enabled = false;
            }
            else
            {
                //renderer.enabled = true;
                if (hit.point!= old_hitPoint)
                {
                    isAdjusted = false; //resetto flag di adjust
                }
            }
            //target.position = (hit.point - transform.forward * offsetFactor * targetScale.x);
            //target.position = (hit.point - transform.forward * offsetFactor * targetScale.x + hit.normal *offsetFactor/2);
            ///////////////////////////////////////////////////////////////

            // Calculate the current distance between the camera and the target object
            float currentDistance = Vector3.Distance(transform.position, target.position);

            // Calculate the ratio between the current distance and the original distance
            float s = currentDistance / originalDistance;

            // Set the scale Vector3 variable to be the ratio of the distances
            targetScale.x = targetScale.y = targetScale.z = s;

            // Set the scale for the target objectm, multiplied by the original scale
            target.localScale = targetScale * originalScale;
        }
    }
}
