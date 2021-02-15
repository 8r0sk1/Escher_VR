using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalingMechanic_new : MonoBehaviour
{
    [Header("Components")]
    public Transform target;            // The target object we picked up for scaling
    public Rigidbody rigidbody;
    private float originalMass;

    [Header("Parameters")]
    public float pickUpDistance;
    public LayerMask targetMask;        // The layer mask used to hit only potential targets with a raycast
    public LayerMask ignoreTargetMask;  // The layer mask used to ignore the player and target objects while raycasting
    public float offsetFactor;          // The offset amount for positioning the object so it doesn't clip into walls

    float originalDistance;             // The original distance between the player camera and the target
    float originalScale;                // The original scale of the target objects prior to being resized
    Vector3 targetScale;                // The scale we want our object to be set to each frame

    private bool isAdjusted;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        HandleInput();
    }

    
    void FixedUpdate()
    {
        ResizeTarget();
    }

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

                    // SETUP della fisica dell'oggetto
                    rigidbody = target.GetComponent<Rigidbody>();
                    rigidbody.useGravity = false;
                    rigidbody.freezeRotation = true;
                    originalMass = rigidbody.mass;
                    rigidbody.mass = 0;
                    isAdjusted = false;

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
                // RESET fisica oggetto
                rigidbody.freezeRotation = false;
                rigidbody.useGravity = true;
                rigidbody.mass = originalMass;
                rigidbody.velocity = Vector3.zero;

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
        if (Physics.Raycast(transform.position, transform.forward, out hit, pickUpDistance, ignoreTargetMask))
        {
            //controllo se la posizione non è aggiustata
            if (!isAdjusted)
            {
                //aggiusto la posizione
                //rigidbody.MovePosition(hit.point + hit.normal * offsetFactor/2 * targetScale.x - transform.forward * offsetFactor * targetScale.x);
                rigidbody.MovePosition(hit.point + hit.normal * offsetFactor * targetScale.x);

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
}
