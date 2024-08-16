using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletOld : MonoBehaviour
{
    public float initialSpeed = 20.0f;
    public float lifeTime = 10.0f;

    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        rb.velocity = initialSpeed * transform.forward;

        Destroy(gameObject, lifeTime);
    }
}
