using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : RecycleObject
{
    // 항상 왼쪽으로 moveSpeed 만큼 이동
    // 수명
    // player 와 부딪히면 터지는 이펙트
    public float moveSpeed = 3.0f;

    private void Start()
    {
        onDisable += () => Factory.Instance.GetExplosion(transform.position);
    }

    private void Update()
    {
        transform.Translate(Time.deltaTime * moveSpeed * Vector2.left);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            DisableTimer();
    }
}
