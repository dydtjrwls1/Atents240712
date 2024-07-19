using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //// 적 프리펩
    //public GameObject enemyPrefabs;

    //// 스폰 코루틴
    //IEnumerator spawnEnemy;

    //// 스폰 간격을 위한 변수
    //WaitForSeconds spawnWait;

    //// 스폰 간격
    //float spawnInterval = 0.5f;

    //// 몬스터 스폰의 X, Y 범위
    //float xRange = 1.0f;
    //float yRange = 4.0f;

    //// 몬스터 스폰의 X, Y 위치
    //float xPos;
    //float yPos;

    //private void Awake()
    //{
    //    spawnEnemy = SpawnEnemy();

    //    spawnWait = new WaitForSeconds(spawnInterval);
    //}

    //// Start is called before the first frame update
    //void Start()
    //{
    //    StartCoroutine(spawnEnemy);
    //}

    //IEnumerator SpawnEnemy()
    //{
    //    while (true)
    //    {
    //        yPos = transform.position.y + Random.Range(-yRange, yRange);
    //        xPos = transform.position.x + Random.Range(-xRange, xRange);
    //        Instantiate(enemyPrefabs, new Vector3(xPos, yPos), transform.rotation);
    //        yield return spawnWait;
    //    }
    //}

    // 적 프리팹
    public GameObject enemyPrefab;

    // 생성 간격
    public float spawnInterval = 1.0f;

    // 최소 높이 최대 높이
    const float MinY = -4.0f;
    const float MaxY = 4.0f;

    const float MinX = -0.5f;
    const float MaxX = 0.5f;


    private void Awake()
    {
        
    }

    private void Start()
    {
        StartCoroutine(SpawnCoroutine()); // 적 생성 시작

        // Time.timeScale = 0.1f; // 게임 진행되는 시간의 스케일 (default = 1)
    }

    /// <summary>
    /// 적을 스폰하는 함수
    /// </summary>
    void Spawn()
    {
        Instantiate(enemyPrefab, GetSpawnPosition(), Quaternion.identity);
    }


    /// <summary>
    /// 적을 주기적으로 스폰하는 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            Spawn();
        }
    }

    /// <summary>
    /// 스폰 될 위치를 계산하는 함수
    /// </summary>
    /// <returns>스폰 될 위치</returns>
    Vector3 GetSpawnPosition()
    {
        Vector3 result = transform.position;
        result.y = Random.Range(MinY, MaxY);
        return result;
    }

    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 p0 = transform.position + Vector3.up * MaxY;
        Vector3 p1 = transform.position + Vector3.up * MinY;

        Gizmos.DrawLine(p0, p1);
    }

    // 선택 됐을 때만 그리기
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Vector3 p0 = transform.position + Vector3.up * MaxY + Vector3.left * 0.5f;
        Vector3 p1 = transform.position + Vector3.up * MaxY + Vector3.right * 0.5f;
        Vector3 p2 = transform.position + Vector3.up * MinY + Vector3.right * 0.5f;
        Vector3 p3 = transform.position + Vector3.up * MinY + Vector3.left * 0.5f;

        Gizmos.DrawLine(p0, p1);
        Gizmos.DrawLine(p1, p2);
        Gizmos.DrawLine(p2, p3);
        Gizmos.DrawLine(p3, p0);

        // new Color(1, 1, 1); // 흰색
        // new Color(1, 0, 0); // 빨강
        // new Color(0, 1, 0); // 초록
        // new Color(0, 0, 1); // 파랑
    }
}
