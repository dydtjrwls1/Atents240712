using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BarBase : MonoBehaviour
{
    public Color color;

    protected Slider slider;
    protected TextMeshProUGUI statusGUI;

    protected float maxValue;

    private void Awake()
    {
        slider = GetComponent<Slider>();

        Transform child = transform.GetChild(0);
        Image background = child.GetComponent<Image>();
        background.color = new Color(color.r, color.g, color.b, color.a * 0.2f);

        child = transform.GetChild(1);
        Image fill = child.GetComponentInChildren<Image>();
        fill.color = color;

        child = transform.GetChild(2);
        statusGUI = child.GetComponent<TextMeshProUGUI>();
    }

    protected void UpdateDisplay(float ratio)
    {
        ratio = Mathf.Clamp01(ratio);
        slider.value = ratio;
        statusGUI.text = $"{ratio * maxValue:f0} / {maxValue}";
    }

}
