using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyEnemy : MonoBehaviour 
{
    // 낙하 속도
    protected float fallSpeed;

    public GameObject explosion;

    // destroy 까지의 시간
    protected const float destroyInterval = 8.0f;

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        Instantiate(explosion, transform.position, Quaternion.identity);
    }
}
