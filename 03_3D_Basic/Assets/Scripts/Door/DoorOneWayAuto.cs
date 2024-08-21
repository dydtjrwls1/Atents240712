using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOneWayAuto : DoorAuto
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.transform.position.z > transform.position.z)
        {
            // 플레이어에서 문으로 향하는 방향 벡터
            Vector3 playerToDoor = transform.position - transform.forward;

            float angle = Vector3.Angle(transform.forward, playerToDoor);
            if (angle > 90.0f)
                Open(); // 사이각이 90도 보다 크면 플레이어가 문의 앞쪽에 있다.
        }
    }
}
