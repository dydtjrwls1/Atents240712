using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO : MyEnemy
{
    float elapsedTime;

    // 사인 함수 주기
    float frequency = 2.0f;

    // 스폰된 위치의 X 좌표
    float spawnX;

    // Start is called before the first frame update
    void Start()
    {
        fallSpeed = 5.0f;
        spawnX = transform.position.x;
        Destroy(gameObject, destroyInterval);
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime * frequency;
        transform.position = new Vector3(spawnX + Mathf.Sin(elapsedTime), transform.position.y - Time.deltaTime * fallSpeed);
    }
}
