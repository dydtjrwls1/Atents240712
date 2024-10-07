using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataManager : MonoBehaviour
{
    public ItemData[] itemDatas;

    // ItemData를 가져오기 위한 인덱서
    public ItemData this[ItemCode code] => itemDatas[(int)code];

    public ItemData this[uint index] => itemDatas[index];
}
