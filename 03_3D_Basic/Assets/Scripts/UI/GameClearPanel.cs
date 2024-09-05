using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameClearPanel : MonoBehaviour
{
    CanvasGroup group;

    Button restartButton;

    Trap_Goal goal;

    private void Awake()
    {
        group = GetComponent<CanvasGroup>();
        restartButton = GetComponentInChildren<Button>();
        restartButton.onClick.AddListener(Restart);
    }

    private void Start()
    {
        goal = FindAnyObjectByType<Trap_Goal>();
        if(goal != null )
        {
            goal.onGoal += GameClear;
        }
    }

    private void Restart()
    {
        SceneManager.LoadScene(0);
    }

    void GameClear()
    {
        group.alpha = 1;
        group.blocksRaycasts = true;
        group.interactable = true;

        GameManager.Instance.VirtualPad?.Disconnect();


    }


}
