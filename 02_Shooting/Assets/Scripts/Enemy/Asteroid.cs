using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : EnemyBase
{
    public float minRotateSpeed = 30.0f;
    public float maxRotateSpeed = 720.0f;

    public float minMoveSpeed = 2.0f;
    public float maxMoveSpeed = 2.0f;

    public AnimationCurve rotateSpeedCurve;

    float moveSpeed;
    float rotateSpeed;

    Vector3 direction;

    private void Start()
    {
        moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);

        rotateSpeed = minRotateSpeed + rotateSpeedCurve.Evaluate(Random.value) * maxRotateSpeed;
    }

    protected override void OnMoveUpdate(float deltaTime)
    {
        transform.Translate(Time.deltaTime * moveSpeed * direction, Space.World);
        transform.Rotate(0, 0, Time.deltaTime * rotateSpeed);
    }
    public void SetDestination(Vector3 destination)
    {
        direction = (destination - transform.position).normalized;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + direction);
    }
}
