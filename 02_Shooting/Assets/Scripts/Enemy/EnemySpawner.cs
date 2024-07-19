using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // 적 프리펩
    public GameObject enemyPrefabs;

    // 스폰 코루틴
    IEnumerator spawnEnemy;

    // 스폰 간격을 위한 변수
    WaitForSeconds spawnWait;

    // 스폰 간격
    float spawnInterval = 0.5f;

    // 몬스터 스폰의 X, Y 범위
    float xRange = 1.0f;
    float yRange = 4.0f;

    // 몬스터 스폰의 X, Y 위치
    float xPos;
    float yPos;

    private void Awake()
    {
        spawnEnemy = SpawnEnemy();

        spawnWait = new WaitForSeconds(spawnInterval);
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnEnemy);
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            yPos = transform.position.y + Random.Range(-yRange, yRange);
            xPos = transform.position.x + Random.Range(-xRange, xRange);
            Instantiate(enemyPrefabs, new Vector3(xPos, yPos), transform.rotation);
            yield return spawnWait;
        }
    }
}
