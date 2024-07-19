using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public float speed = 1.0f;

    float elapsedTime;

    float yPos;

    float frequency = 0.5f;

    float amplitude = 0.15f;
    private void Start()
    {
        yPos = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime * frequency;
        transform.position = new Vector3(transform.position.x, yPos + Mathf.Sin(elapsedTime) * amplitude, 0);
    }
}
