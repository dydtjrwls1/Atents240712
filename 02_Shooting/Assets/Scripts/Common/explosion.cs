using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class explosion : RecycleObject
{
    Animator animator;

    float clipLength = 0.0f;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        // animator.GetCurrentAnimatorClipInfo(0); : 애니메이션 컨트롤러의 첫 번째 레이어가 가지고 있는 클립 정보들 가져오기.
        // animator.GetCurrentAnimatorClipInfo(0)[0]; : 클립 정보들 중에서 첫 번째 클립의 정보

        AnimatorClipInfo info = animator.GetCurrentAnimatorClipInfo(0)[0]; // 하나만 존재하는 것을 알고 있어서 첫 번째 것 가져오기
        clipLength = info.clip.length;

        // Destroy(gameObject, info.clip.length); // 애니메이션 클립 재생 시간이 끝나면 삭제
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        DisableTimer(clipLength);
    }
}
