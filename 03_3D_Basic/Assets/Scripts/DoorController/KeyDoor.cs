using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : MonoBehaviour
{
    public float rotateSpeed = 360.0f;

    public DoorKey targetDoor;

    Transform model;

    private void Awake()
    {
        model = transform.GetChild(0);
    }

    private void Update()
    {
        model.Rotate(Vector3.up, Time.deltaTime * rotateSpeed, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(targetDoor != null)
            {
                targetDoor.UnLock();
                gameObject.SetActive(false);
            }
            else
            {
                Debug.LogWarning("문이 없음.");
            }
            
        }
    }

}
