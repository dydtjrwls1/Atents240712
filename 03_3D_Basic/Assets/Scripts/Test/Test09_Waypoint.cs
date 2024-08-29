using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test09_Waypoint : TestBase
{
    Player player;
    Transform position;

    private void Start()
    {
        position = transform.GetChild(0);
        player = FindAnyObjectByType<Player>();
    }

    protected override void Test1_performed(InputAction.CallbackContext context)
    {
        player.transform.position = position.transform.position;
        player.transform.position += Vector3.up;
    }
}
