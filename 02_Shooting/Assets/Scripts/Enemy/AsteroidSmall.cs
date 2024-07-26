using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSmall : Asteroid
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Factory.Instance.GetExplosion(transform.position);
        gameObject.SetActive(false);
    }
}
