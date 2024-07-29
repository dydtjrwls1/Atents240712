using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultySpawner : MonoBehaviour
{
    public enum SpawnType
    {
        Wave = 0,
        Asteroid
    }

    [Serializable]
    public struct SpawnData
    {
        public SpawnType type;
        public float interval;
    }

    // 스폰할 종류의 적과 스폰 간격을 저장해 놓은 배열
    public SpawnData[] spawnDatas;

    // 최소 높이 최대 높이
    protected const float MinY = -4.0f;
    protected const float MaxY = 4.0f;

    // 목적지 ( 목적지가 필요한 적용 )
    Transform destinationArea;

    private void Awake()
    {
        destinationArea = transform.GetChild(0);
    }

    private void Start()
    {
        foreach(var data in spawnDatas)
        {
            StartCoroutine(SpawnCoroutine(data)); // 데이터 별로 코루틴 실행
        }
    }
    IEnumerator SpawnCoroutine(SpawnData data)
    {
        while (true)
        {
            yield return new WaitForSeconds(data.interval);
            
            switch (data.type)
            {  
                case SpawnType.Wave:
                    Factory.Instance.GetEnemyWave(GetSpawnPosition());
                    break;
                case SpawnType.Asteroid:
                    EnemyAsteroidBig big = Factory.Instance.GetEnemyAsteroidBig(GetSpawnPosition());
                    big.SetDestination(GetDestination());
                    break;
            }  
        }
    }

    protected Vector3 GetSpawnPosition()
    {
        Vector3 result = transform.position;
        result.y = UnityEngine.Random.Range(MinY, MaxY);
        return result;
    }
    Vector3 GetDestination()
    {
        Vector3 pos = destinationArea.position;
        pos.y += UnityEngine.Random.Range(MinY, MaxY);

        return pos;
    }

    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 p0 = transform.position + Vector3.up * MaxY;
        Vector3 p1 = transform.position + Vector3.up * MinY;

        Gizmos.DrawLine(p0, p1);

        if (destinationArea == null)
            destinationArea = transform.GetChild(0);

        Gizmos.color = Color.yellow;
        p0 = destinationArea.position + Vector3.up * MaxY;
        p1 = destinationArea.position + Vector3.up * MinY;

        Gizmos.DrawLine(p0, p1);
    }

    protected void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Vector3 p0 = destinationArea.position + Vector3.up * MaxY + Vector3.left * 0.5f;
        Vector3 p1 = destinationArea.position + Vector3.up * MaxY + Vector3.right * 0.5f;
        Vector3 p2 = destinationArea.position + Vector3.up * MinY + Vector3.right * 0.5f;
        Vector3 p3 = destinationArea.position + Vector3.up * MinY + Vector3.left * 0.5f;

        Gizmos.DrawLine(p0, p1);
        Gizmos.DrawLine(p1, p2);
        Gizmos.DrawLine(p2, p3);
        Gizmos.DrawLine(p3, p0);

        Gizmos.color = Color.red;

        p0 = transform.position + Vector3.up * MaxY + Vector3.left * 0.5f;
        p1 = transform.position + Vector3.up * MaxY + Vector3.right * 0.5f;
        p2 = transform.position + Vector3.up * MinY + Vector3.right * 0.5f;
        p3 = transform.position + Vector3.up * MinY + Vector3.left * 0.5f;

        Gizmos.DrawLine(p0, p1);
        Gizmos.DrawLine(p1, p2);
        Gizmos.DrawLine(p2, p3);
        Gizmos.DrawLine(p3, p0);
    }
}
