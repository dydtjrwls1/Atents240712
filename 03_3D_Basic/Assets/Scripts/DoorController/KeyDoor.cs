using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : MonoBehaviour
{
    // 열쇠 회전 속도
    public float rotateSpeed = 360.0f;

    // 열쇠가 열 문
    public DoorBase targetDoor;

    // 문의 잠금해제 인터페이스
    IUnlockable unlockable;

    // 메시 모델의 트랜스폼
    Transform model;

    private void Awake()
    {
        model = transform.GetChild(0);
        // as : as 왼쪽에 있는 변수가 as  오른쪽에 있는 타입으로 변경이 가능하면 null 이 아닌값, 변경이 불가능하면 null 이 된다.
        unlockable = targetDoor as IUnlockable; 
        if(unlockable == null) // 잠금해제 가능한 문이 아니면
        {
            targetDoor = null;
            Debug.LogWarning("잠금해제가 불가능한 문입니다.");
        }
    }

    private void Update()
    {
        model.Rotate(Vector3.up, Time.deltaTime * rotateSpeed, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // 플레이어가 먹었을 때
        {
            if(unlockable != null)
            {
                unlockable.Unlock(); // 잠금 해제가능한 문이 등록되어 있으면 잠금해제
                
            }
            else
                Debug.LogWarning("문이 없음.");
            Destroy(gameObject); // 먹은 열쇠 사라지기
        }
    }
}
