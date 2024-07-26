using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class EnemyObjectPool<T> : ObjectPool<T> where T : EnemyBase
{
    // 점수 표시용 UI
    ScoreText scoreText;

    /// <summary>
    /// 적이 하나 생성될 때 실행되는 함수
    /// </summary>
    /// <param name="comp">생성된 적 하나</param>
    protected override void OnGenerateObject(T comp)
    {
        if(scoreText != null)
        {
            comp.onDie += scoreText.AddScore; // 사망 델리게이트에 점수표시 UI의 함수를 등록
        }
    }

    public override void Initialize()
    {
        base.Initialize();
        scoreText = FindAnyObjectByType<ScoreText>();
    }
}
