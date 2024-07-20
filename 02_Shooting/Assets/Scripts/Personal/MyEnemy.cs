using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyEnemy : Poop
{
    public float speed = 2.0f;

    float elapsedTime;

    float frequency = 2.0f;

    float spawnX;
    private void Awake()
    {
        spawnX = transform.position.x;
    }
    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime * frequency;
        transform.position = new Vector3(spawnX + Mathf.Sin(elapsedTime), transform.position.y - Time.deltaTime * speed);
    }
}
