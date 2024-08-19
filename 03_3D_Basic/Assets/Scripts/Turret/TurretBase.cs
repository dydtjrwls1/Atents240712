using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBase : MonoBehaviour
{
    public float fireInterval = 0.5f;

    // 총열 트랜스폼
    protected Transform gun;

    protected IEnumerator fire;

    Transform firePosition;

    protected virtual void Awake()
    {
        fire = Fire(fireInterval);
        gun = transform.GetChild(2);
        firePosition = gun.GetChild(0);
    }

    IEnumerator Fire(float interval)
    {
        while (true)
        {
            Factory.Instance.GetBullet(firePosition.position, firePosition.eulerAngles);
            yield return new WaitForSeconds(interval);
        }
        
    }

#if UNITY_EDITOR
    protected virtual void OnDrawGizmos()
    {
        Vector3 p0 = transform.position + transform.up * 0.01f;
        Vector3 p1 = p0 + transform.forward * 2.0f;
        Vector3 p2 = p1 + Quaternion.AngleAxis(150, Vector3.up) * transform.forward * 0.2f;
        Vector3 p3 = p1 + Quaternion.AngleAxis(-150, Vector3.up) * transform.forward * 0.2f;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(p0, p1);
        Gizmos.DrawLine(p1, p2);
        Gizmos.DrawLine(p1, p3);
    }
#endif
}
