using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    Transform player;

    private void Start()
    {
        player = FindAnyObjectByType<Player>().transform;
    }

    private void LateUpdate()
    {
        transform.position = player.position + (-player.forward * 5) + (player.up * 4.5f);
        transform.rotation = Quaternion.Euler(15, player.rotation.eulerAngles.y, 0);
    }
}
