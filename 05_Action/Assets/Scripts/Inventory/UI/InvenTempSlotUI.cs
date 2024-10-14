using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InvenTempSlotUI : SlotUI_Base
{
    private void Update()
    {
        transform.position = Mouse.current.position.ReadValue();
    }
}
