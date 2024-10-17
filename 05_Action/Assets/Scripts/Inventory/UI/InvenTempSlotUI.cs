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

    public void ItemDrop(Vector2 screen)
    {
        // 1. 아이템이 있을 때만 처리
        // 2. screen 좌표가 가리키는 바닥 주변에 뿌림
        // 3. 플레이어 위치에서 itemPickupRange 반경안에 아이템이 드랍되어야 한다.
        // 4. 아이템1개 드랍할 때는 노이즈 없음
        ItemData data = InvenSlot.ItemData;

        // TempSlot 에 아이템이 있을 경우만 실행
        if(data != null)
        {
            Camera cam = Camera.main;
            Player player = GameManager.Instance.InventoryUI.Owner;

            // Screen 좌표를 World 좌표로 변환
            Vector3 screenPosition = screen;
            screenPosition.z = player.transform.position.z - cam.transform.position.z; 

            Vector3 worldPosition = cam.ScreenToWorldPoint(screenPosition);
            worldPosition = Vector3.ClampMagnitude(worldPosition, player.ItemPickUpRange); // 최대범위는 아이템 픽업범위까지

            // 아이템의 개수가 2개 이상이면 Noise 를 적용한다.
            bool useNoise = InvenSlot.ItemCount > 1 ? true : false;

            Factory.Instance.MakeItems(data.code, InvenSlot.ItemCount, worldPosition, useNoise);

            InvenSlot.ClearSlotItem(); // 아이템 뿌렸으면 슬롯 비우기
        }
    }
}
