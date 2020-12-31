using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIadjust : MonoBehaviour
{

    public GameObject axisReference;
    public float rotationSpeed;
    private float rotation = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rotation = Vector3.SignedAngle(this.transform.up, axisReference.transform.up, axisReference.transform.right);
        Debug.Log("Rotation = " + rotation);

        if (rotation != 0f)
            this.transform.RotateAround(this.transform.position, this.transform.right, rotation * rotationSpeed * Time.deltaTime);
    }
}
