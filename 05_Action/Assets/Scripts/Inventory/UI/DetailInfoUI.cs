using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class DetailInfoUI : MonoBehaviour
{
    public float alphaChangeSpeed = 10.0f;

    // 디테일 정보 창이 화면밖을 벗어나면 안된다.
    // 아이템을 옮기는 도중에는 보이면 안된다.

    RectTransform rectTransform;

    CanvasGroup group;

    TextMeshProUGUI nameGUI;
    TextMeshProUGUI descriptionGUI;
    TextMeshProUGUI priceGUI;

    Image icon;

    Coroutine fadeOutCoroutine = null;
    Coroutine fadeInCoroutine = null;

    bool isClicked = false;

    

    private void Awake()
    {
        group = GetComponent<CanvasGroup>();
        
        Transform child = transform.GetChild(0);
        icon = child.GetComponent<Image>();

        child = transform.GetChild(1);
        nameGUI = child.GetComponent<TextMeshProUGUI>();

        child = transform.GetChild(2);
        priceGUI = child.GetComponent<TextMeshProUGUI>();

        child = transform.GetChild(4);
        descriptionGUI = child.GetComponent<TextMeshProUGUI>();

        rectTransform = transform as RectTransform;
    }

    private void Start()
    {
        group.alpha = 0.0f;
    }

    public void OnItemDetailInfoUp()
    {
        isClicked = false;
    }

    public void OnItemDetailInfoDown(bool isEmpty)
    {
        if (!isEmpty)
        {
            group.alpha = 0.0f;
            isClicked = true;
        }
    }

    public void MovePosition(Vector2 screen)
    {
        if(group.alpha > 0.01f)
        {
            int over = (int)(screen.x + rectTransform.rect.width) - Screen.width;
            screen.x -= Mathf.Max(0, over);
            transform.position = screen;

            //if (screen.x + rectTransform.rect.width > Screen.width)
            //{
            //    transform.position = new Vector2(Screen.width - rectTransform.rect.width, screen.y);
            //}
            //else
            //{
            //    transform.position = screen;
            //}
        }
    }

    public void OnItemDetailInfoOpen(ItemData data)
    {
        if(data != null && !isClicked)
        {
            StopRunningCoroutine(fadeOutCoroutine); // 현재 실행중인 FadeOut 코루틴 정지

            fadeInCoroutine = StartCoroutine(FadeIn());

            MovePosition(Mouse.current.position.value);

            // detailInfo 창 정보 업데이트
            icon.sprite = data.itemIcon;
            nameGUI.text = data.name;
            priceGUI.text = data.price.ToString("N0");
            descriptionGUI.text = data.itemDesc;
        }
    }

    public void OnItemDetailInfoClose(bool isEmpty)
    {
        if (!isEmpty && !isClicked)
        {
            StopRunningCoroutine(fadeInCoroutine); // FadeIn 중이라면 정지

            fadeOutCoroutine = StartCoroutine(FadeOut());
        }
    }

    IEnumerator FadeIn()
    {
        while (group.alpha < 1.0f)
        {
            group.alpha += Time.deltaTime * alphaChangeSpeed;
            yield return null;
        }

        group.alpha = 1.0f;
        fadeInCoroutine = null;
    }

    IEnumerator FadeOut()
    {
        group.alpha = 1.0f;

        while (group.alpha > 0.01f)
        {
            group.alpha -= Time.deltaTime * alphaChangeSpeed;
            yield return null;
        }

        group.alpha = 0.0f;
        fadeOutCoroutine = null;
    }

    private void StopRunningCoroutine(Coroutine coroutine)
    {
        if(coroutine != null)
        {
            StopCoroutine(coroutine);
        }
    }
}
