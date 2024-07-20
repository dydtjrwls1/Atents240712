using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoopSpawner : MonoBehaviour
{
    // 스폰 코루틴
    IEnumerator spawnCoroutine;

    // 똥 프리펩
    public GameObject poopPrefab;

    // 거대 똥 프리펩
    public GameObject largePoopPrefab;

    // 적 비행체 프리펩
    public GameObject EnemyPrefab;

    // 프리펩 array
    GameObject[] poopArray = new GameObject[3];

    // 스폰 Y 좌표
    const float yPos = 5.3f;

    // 스폰 X 좌표
    float xPos;

    // 스폰 X 범위
    float xRange = 9.0f;

    // 스폰 좌표
    Vector2 spawnPos;

    // 스폰 간격
    public float spawnInterval = 1.0f;

    private void Awake()
    {
        spawnCoroutine = SpawnPoop();

        poopArray[0] = poopPrefab;
        poopArray[1] = largePoopPrefab;
        poopArray[2] = EnemyPrefab;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnCoroutine);
    }

    IEnumerator SpawnPoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            xPos = Random.Range(-xRange, xRange);
            spawnPos = new Vector2(xPos, yPos);
            int index = Random.Range(0, 3);
            GameObject spawnEnemy = poopArray[index];
            Instantiate(spawnEnemy, spawnPos, spawnEnemy.transform.rotation);
        }
    }
}
