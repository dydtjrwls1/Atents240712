using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaBar : BarBase
{
    void Start()
    {
        PlayerStatus status = GameManager.Instance.Status;

        if (status != null)
        {
            maxValue = status.MaxMP;
            slider.value = 1.0f;
            statusGUI.text = $"{maxValue} / {maxValue}";
            status.onManaChange += UpdateDisplay;
        }
    }
}
