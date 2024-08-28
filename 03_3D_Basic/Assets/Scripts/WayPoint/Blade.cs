using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : WayPointUserBase
{
    public float spinSpeed = 720;

    Transform bladeMesh;

    protected override Transform Target { 
        get => base.Target; 
        set 
        {
            base.Target = value; 
            transform.LookAt(Target.position);
        }

    }

    protected override void Awake()
    {
        base.Awake();
        bladeMesh = transform.GetChild(0);
    }

    private void Update()
    {
        bladeMesh.Rotate(Time.deltaTime * spinSpeed * Vector3.right);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.gameObject.GetComponent<Player>();
            player.Die();
        }
    }
}
