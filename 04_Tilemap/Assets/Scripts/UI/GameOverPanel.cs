using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    public float alphaChangeSpeed = 1.0f;

    CanvasGroup group;

    TextMeshProUGUI playTimeTMP;
    TextMeshProUGUI killCountTMP;

    Button restartButton;
    Image buttonImage;


    private void Awake()
    {
        group = GetComponent<CanvasGroup>();

        Transform child = transform.GetChild(1);
        playTimeTMP = child.GetComponent<TextMeshProUGUI>();

        child = transform.GetChild(2);
        killCountTMP = child.GetComponent<TextMeshProUGUI>();

        child = transform.GetChild(3);
        restartButton = child.GetComponent<Button>();
        buttonImage = child.GetComponent<Image>();
    }

    private void Start()
    {
        Player player = GameManager.Instance.Player;
        player.onDie += OnDie;

        restartButton.onClick.AddListener(Restart);
    }

    private void Restart()
    {
        StartCoroutine(WaitUnloadAll());
    }

    private void OnDie()
    {
        StartCoroutine(ScreenActivate());
    }

    IEnumerator ScreenActivate()
    {
        Player player = GameManager.Instance.Player;

        playTimeTMP.text = $"Total Play Time\n\r< {player.PlayTime:f1} Sec >";
        killCountTMP.text = $"Total Kill Count\n\r< {player.KillCount} Kill >";

        while (group.alpha < 1.0f)
        {
            group.alpha += Time.deltaTime * alphaChangeSpeed;
            yield return null;
        }

        group.alpha = 1.0f;
        group.blocksRaycasts = true;
        group.interactable = true;
    }

    IEnumerator WaitUnloadAll()
    {
        SubmapManager submap = GameManager.Instance.SubmapManager;

        while (!submap.IsUnloadAll)
        {
            yield return null;  
        }

        SceneManager.LoadScene(0);
    }
}
