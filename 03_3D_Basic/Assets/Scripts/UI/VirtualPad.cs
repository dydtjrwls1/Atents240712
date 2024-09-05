using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualPad : MonoBehaviour
{
    VirtualStick[] sticks;

    VirtualButton[] buttons;

    private void Awake()
    {
        sticks = GetComponentsInChildren<VirtualStick>();
        buttons = GetComponentsInChildren<VirtualButton>();
    }

    public VirtualStick GetStick(int index)
    {
        return sticks[index];
    }

    public VirtualButton GetButton(int index)
    {
        return buttons[index];
    }

    public void SetStickBind(int index, Action<Vector2> onInput)
    {
        sticks[index].onMoveInput = onInput;
    }

    public void SetButtonBind(int index, Action onClick, ref Action<float> onCoolTimeChange)
    {
        buttons[index].onClick = onClick;

        onCoolTimeChange += buttons[index].RefreshCoolTime;
    }

    public void Disconnect()
    {
        foreach (var stick in sticks)
        {
            stick.onMoveInput = null;
        }

        foreach (var button in buttons)
        {
            button.onClick = null;
        }
    }
}
