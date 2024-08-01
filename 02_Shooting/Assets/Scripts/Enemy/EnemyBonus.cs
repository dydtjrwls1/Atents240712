using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyBonus : EnemyBase
{
    float elapsedTime = 0.0f;

    float xBorder = 7.0f;

    float waitTime = 3.0f;

    float accelSpeed = 10.0f;

    protected override void OnMoveUpdate(float deltaTime)
    {
        if( transform.position.x > xBorder)
        {
            base.OnMoveUpdate(deltaTime);
        } else
        {
            elapsedTime += deltaTime;
            if (elapsedTime > waitTime)
            {
                speed = accelSpeed;
                base.OnMoveUpdate(deltaTime);
            }
        }
    }

    protected override void OnDie()
    {
        Factory.Instance.GetPowerUp(transform.position);
    }
}
