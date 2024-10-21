using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : BarBase
{
    void Start()
    {
        PlayerStatus status = GameManager.Instance.Status;

        if(status != null )
        {
            maxValue = status.MaxHP;
            slider.value = 1.0f;
            statusGUI.text = $"{maxValue} / {maxValue}";
            status.onHealthChange += UpdateDisplay;
        }
    }
}
