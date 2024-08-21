using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOneWayAuto : DoorBase
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.transform.position.z > transform.position.z )
            Open();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            Close();
    }
}
