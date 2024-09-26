using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingleTon<GameManager>
{
    Player player;

    SubmapManager submapManager;

    public SubmapManager SubmapManager => submapManager;

    public Player Player
    {
        get
        {
            if (player == null)
            {
                player = FindAnyObjectByType<Player>();
            }

            return player;
        }
    }

    protected override void OnInitialize()
    {
        player = FindAnyObjectByType<Player>();
        submapManager.Initialize();
    }

    protected override void OnPreInitialize()
    {
        base.OnPreInitialize();
        submapManager = GetComponent<SubmapManager>();
        submapManager.PreInitialize();
    }
}
