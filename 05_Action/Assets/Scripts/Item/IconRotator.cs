using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconRotator : MonoBehaviour
{
    public float rotateSpeed = 360.0f;
    public float moveSpeed = 2.0f;

    public float minHeight = 0.5f;
    public float maxHeight = 1.5f;

    float m_ElapsedTime = 0.0f;


    void Start()
    {
        transform.Rotate(0, Random.Range(0f, 360.0f), m_ElapsedTime); // 초기 회전각도 랜덤 설정    
        transform.position = transform.parent.position + Vector3.up * maxHeight;
    }

    // Update is called once per frame
    void Update()
    {
        m_ElapsedTime += Time.deltaTime * moveSpeed;

        Vector3 pos;
        pos.x = transform.parent.position.x;
        pos.z = transform.parent.position.z;
        pos.y = minHeight + ((Mathf.Cos(m_ElapsedTime) + 1) * 0.5f) * (maxHeight - minHeight);

        transform.position = pos;

        transform.Rotate(0, Time.deltaTime * rotateSpeed, 0);
    }
}
