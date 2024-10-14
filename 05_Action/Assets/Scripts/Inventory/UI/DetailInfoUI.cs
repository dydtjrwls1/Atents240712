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

    public void OnItemDetailInfoUp(ItemData data)
    {
        isClicked = false;

        if (data != null)
        {
            StopRunningCoroutine();

            OnItemDetailInfoOpen(data);
        }
    }

    public void OnItemDetailInfoDown(bool isEmpty)
    {
        if (!isEmpty)
        {
            StopRunningCoroutine();

            group.alpha = 0.0f;
            isClicked = true;
        }
    }

    public void OnItemDetailInfoMove(Vector2 position)
    {
        if(position.x + rectTransform.rect.width > Screen.width)
        {
            transform.position = new Vector2(Screen.width - rectTransform.rect.width, position.y);
        }
        else
        {
            transform.position = position;
        }
    }

    public void OnItemDetailInfoOpen(ItemData data)
    {
        if(data != null && !isClicked)
        {
            StopRunningCoroutine();

            group.alpha = 1.0f;

            icon.sprite = data.itemIcon;
            nameGUI.text = data.name;
            priceGUI.text = data.price.ToString();
            descriptionGUI.text = data.itemDesc;
        }
    }

    public void OnItemDetailInfoClose(bool isEmpty)
    {
        if (!isEmpty && !isClicked)
        {
            StopRunningCoroutine();

            fadeOutCoroutine = StartCoroutine(FadeOut());
        }
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

    private void StopRunningCoroutine()
    {
        if(fadeOutCoroutine != null)
        {
            StopCoroutine(fadeOutCoroutine);
        }
    }
}
