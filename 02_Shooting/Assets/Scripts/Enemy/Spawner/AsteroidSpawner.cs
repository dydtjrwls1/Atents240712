using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AsteroidSpawner : EnemySpawner
{
    Transform destinationArea;

    private void Awake()
    {
        destinationArea = transform.GetChild(0);
    }

    protected override void Spawn()
    {
        EnemyAsteroidBig asteroid = Factory.Instance.GetEnemyAsteroidBig(GetSpawnPosition());
        asteroid.SetDestination(GetDestination());
    }

#if UNITY_EDITOR
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        if(destinationArea == null)
            destinationArea = transform.GetChild(0);

        Gizmos.color = Color.yellow;
        Vector3 p0 = destinationArea.position + Vector3.up * MaxY;
        Vector3 p1 = destinationArea.position + Vector3.up * MinY;

        Gizmos.DrawLine(p0, p1);
    }

    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        Gizmos.color = Color.red;

        Vector3 p0 = destinationArea.position + Vector3.up * MaxY + Vector3.left * 0.5f;
        Vector3 p1 = destinationArea.position + Vector3.up * MaxY + Vector3.right * 0.5f;
        Vector3 p2 = destinationArea.position + Vector3.up * MinY + Vector3.right * 0.5f;
        Vector3 p3 = destinationArea.position + Vector3.up * MinY + Vector3.left * 0.5f;

        Gizmos.DrawLine(p0, p1);
        Gizmos.DrawLine(p1, p2);
        Gizmos.DrawLine(p2, p3);
        Gizmos.DrawLine(p3, p0);
    }
#endif
    Vector3 GetDestination()
    {
        Vector3 pos = destinationArea.position;
        pos.y += Random.Range(MinY, MaxY);

        return pos;
    }
}
