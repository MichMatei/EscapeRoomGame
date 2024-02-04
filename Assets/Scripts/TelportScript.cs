using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelportScript : MonoBehaviour
{
    public Transform teleportDestination;
    public Transform objectToTeleport;
    // Update is called once per frame

    private void OnTriggerEnter(Collider other)
    {
        objectToTeleport.transform.position = teleportDestination.transform.position;
    }
}
