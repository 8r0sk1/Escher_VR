using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleporter : MonoBehaviour
{
    public Transform player;
    public Transform reciever;

    private bool playerIsOverlapping = false;
    void Update()
    {
        if (playerIsOverlapping && reciever != null)
        {
            //Debug.Log("Overlapping " + this.name);
            //controllo che il player entri dalla parte giusta del portale
            Vector3 portalToPlayer = player.position - transform.position;
            float dotProduct = Vector3.Dot(transform.up, portalToPlayer);

            if (dotProduct < 0f)
            {
                //teleporto il player
                float rotationDiff = -Quaternion.Angle(transform.rotation, reciever.rotation);
                rotationDiff += 180;
                player.Rotate(Vector3.up, rotationDiff);

                Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer;
                player.position = reciever.position + positionOffset;

                playerIsOverlapping = false;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            //Debug.Log("Portal triggered\n");
            playerIsOverlapping = true;

        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            //Debug.Log("Portal triggered\n");
            playerIsOverlapping = false;

        }
    }
}
