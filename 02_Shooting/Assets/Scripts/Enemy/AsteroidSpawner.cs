using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnAsteroid());
    }

    IEnumerator SpawnAsteroid()
    {
        while (true)
        {
            Factory.Instance.GetAsteroid(transform.position);
            yield return new WaitForSeconds(0.5f);
        } 
    }
}
