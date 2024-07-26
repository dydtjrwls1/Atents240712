using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Asteroid : RecycleObject
{
    public float minRotateSpeed = 30.0f;
    public float maxRotatespeed = 720.0f;

    public float minMoveSpeed = 2.0f;
    public float maxMoveSpeed = 2.0f;

    public AnimationCurve rotateSpeedCurve;

    protected float moveSpeed;
    protected float rotateSpeed;

    protected Vector3 direction;

    protected virtual void Start()
    {
        moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);

        rotateSpeed = minRotateSpeed + rotateSpeedCurve.Evaluate(Random.value) * maxRotatespeed;
    }

    // Update is called once per frame
    protected void Update()
    {   // Rotate 함수 활용법 : 원래 회전에서 추가로 회전
        // transform.Rotate(0, 0, Time.deltaTime * 360);                // x,y,x 따로 받기
        // transform.Rotate(Time.deltaTime * 360 * Vector3.forward);    // vector3 로 받기
        // transform.Rotate(Vector3.forward, Time.deltaTime * 360);     // 축과 축을 중심으로 얼마나 회전할지
        // transform.Translate(Time.deltaTime * moveSpeed * Vector3.left, Space.World);
        // transform.Rotate(Time.deltaTime * rotateSpeed * Vector3.forward);

        // Quaternion 활용법 :
        // Quaternion.Euler(0, 0, 30); // z 축으로 30도 만큼 회전
        // transform.rotation *= Quaternion.Euler(0, 0, 30); // 원래 회전에서 추가로 z축 30도 회전

        // Quaternion.LookRotation(Vector3.forward); // z축 방향을 바라보는 회전 만들기
        // Quaternion.AngleAxis(angle, axis) // 특정 축을 기준으로 angle 만큼 돌아가는 회전

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Factory.Instance.GetExplosion(transform.position);

        int randAngle = Random.Range(0, 90);

        for (int i = 0; i < 4; i++)
        {
            
            float angle = 90.0f;
            Quaternion rotation = Quaternion.Euler(0, 0, randAngle + i * angle);
            SpawnAsteroidSmall(rotation * new Vector3(1, 1));
        }

        gameObject.SetActive(false);
    }

    private void SpawnAsteroidSmall(Vector3 fragmentDirection)
    {
        AsteroidSmall asteroidSmall = Factory.Instance.GetAsteroidSmall(transform.position);
        asteroidSmall.SetDestination(transform.position + fragmentDirection);
    }
}
