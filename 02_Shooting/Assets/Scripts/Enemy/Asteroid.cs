using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float rotateSpeed = 360.0f;

    // Update is called once per frame
    void Update()
    {
        // transform.Rotate(0, 0, Time.deltaTime * 360);                // x,y,x 따로 받기
        // transform.Rotate(Time.deltaTime * 360 * Vector3.forward);    // vector3 로 받기
        // transform.Rotate(Vector3.forward, Time.deltaTime * 360);     // 축과 축을 중심으로 얼마나 회전할지
        transform.Translate(Time.deltaTime * moveSpeed * Vector3.left, Space.World);
        transform.Rotate(Time.deltaTime * rotateSpeed * Vector3.forward);
        
    }
}
