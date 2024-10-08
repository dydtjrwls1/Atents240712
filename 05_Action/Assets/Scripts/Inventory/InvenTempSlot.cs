using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InvenTempSlot : InvenSlot
{
    // 임시 슬롯용 인덱스 (안 쓰는 숫자)
    public const uint TempSlotIndex = 99999999;

    // 드래그를 시작한 슬롯의 인덱스 (null 이면 드래그가 시작 안되었음)
    // 슬롯이 아닌 영역에서 드래그가 끝났을 때 원래 슬롯으로 돌아가기 위해 필요함
    public uint? FromIndex { get; set; } // (FromIndex라는 변수가 있는것처럼 처리한다.)
    
    // 위와 똑같은 기능을 한다.
    //public uint? FromIndex
    //{
    //    get => FromIndex;
    //    set => FromIndex = value;
    //}

    public InvenTempSlot() : base(TempSlotIndex)
    {
        FromIndex = null;
    }

    public override void ClearSlotItem()
    {
        base.ClearSlotItem();
        FromIndex = null;
    }
}
