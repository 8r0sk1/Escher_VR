using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCamera : MonoBehaviour
{
    public Transform playerCamera; //camera del giocatore
    public Transform portal; //posizione del portale
    public Transform otherPortal; //posizione dell'altro portale

    // Update is called once per frame
    void Update()
    {
        //calcolo la posizione che ha di distanza il player dal portale che ha davanti
        Vector3 playerOffsetFromPortal = playerCamera.position - otherPortal.position;
        //sposto la camera che guarda il portale nella stessa posizione da cui osserva il player
        transform.position = portal.position + playerOffsetFromPortal;

        //calcolo l'angolazione che il player ha rispetto al portale
        float angularDifferenceBetweenPortalRotations = Quaternion.Angle(portal.rotation,otherPortal.rotation);
        //ruoto la camera che osserva l'altro ambiente dello stesso angolo
        Quaternion portalRotationalDifference = Quaternion.AngleAxis(angularDifferenceBetweenPortalRotations, Vector3.up);
        Vector3 newCameraDirection = portalRotationalDifference * playerCamera.forward;
        transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up);
    }   
}
