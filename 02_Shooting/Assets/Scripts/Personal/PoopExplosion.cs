using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoopExplosion : MonoBehaviour
{
    Animator animator;

    AnimatorClipInfo info;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        info = animator.GetCurrentAnimatorClipInfo(0)[0];
    }
    // Start is called before the first frame update
    void Start()
    {
        float playTime = info.clip.length;
        Destroy(gameObject, playTime);
    }
}
