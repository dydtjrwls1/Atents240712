using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test04_Bullet : TestBase
{
    public GameObject simpleBulletPrefab;

    Transform fire;

    private void Start()
    {
        fire = transform.GetChild(0);
    }

    protected override void Test1_performed(InputAction.CallbackContext context)
    {
        Instantiate(simpleBulletPrefab, fire.transform);
    }

    protected override void Test2_performed(InputAction.CallbackContext context)
    {
        Factory.Instance.GetBullet(fire.transform.position);
        Time.timeScale = 0.1f;
    }
}
