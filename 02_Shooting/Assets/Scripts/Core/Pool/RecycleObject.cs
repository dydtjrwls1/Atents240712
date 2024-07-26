using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RecycleObject : MonoBehaviour
{
    // 델리게이트 - 함수를 저장할 수 있는 데이터 타입. 주 사용용도 => 특정 상황임을 알리기 위해 사용.
    // Action  : 파라메터와 리턴이 없는 함수만 저장 가능한 델리게이트 (C# 이 쓰기 편하게 미리 만들어 놓은 델리게이트)
    // Func<T> : 리턴은 반드시 있고 파라메터도 설정 가능한 델리게이트

    /// <summary>
    /// 재활용 오브젝트가 비활성화 될 때 실행되는 델리게이트
    /// </summary>
    public Action onDisable = null;

    protected virtual void OnEnable()
    {
        transform.localPosition = Vector3.zero;         // 위치와 회전 초기화
        transform.localRotation = Quaternion.identity;  

        StopAllCoroutines();    // 이전에 실행중이던 코루틴 모두 정지
    }

    protected virtual void OnDisable()
    {
        onDisable?.Invoke();    // onDisable 이 null 이 아니면 실행하라.

    }

    /// <summary>
    /// 일정 시간 후에 게임 오브젝트를 자동으로 비활성화시키는 함수
    /// </summary>
    /// <param name="time">기다릴 시간</param>
    protected void DisableTimer(float time = 0.0f)
    {
        StartCoroutine(LifeOver(time));
    }

    protected IEnumerator LifeOver(float time = 0.0f)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}
