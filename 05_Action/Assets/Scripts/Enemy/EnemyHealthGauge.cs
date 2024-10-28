using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthGauge : MonoBehaviour
{
    // 빌보드로 구성한다
    Transform fillPivot;

    private void Awake()
    {
        fillPivot = transform.GetChild(1);
    }

    private void Start()
    {
        IHealth health = GetComponentInParent<IHealth>();
        health.onHealthChange += UpdateGaugeDisplay;
    }

    void UpdateGaugeDisplay(float ratio)
    {
        fillPivot.localScale = new Vector3(ratio, 1f, 1f);
    }

    private void LateUpdate()
    {
        transform.forward = Camera.main.transform.forward;
    }
}
