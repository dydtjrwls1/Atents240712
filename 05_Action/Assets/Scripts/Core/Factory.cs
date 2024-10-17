using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Factory : SingleTon<Factory>
{
    public float spawnNoise = 0.5f;

    ItemDataManager itemDataManager;

    ItemPool itemPool;

    protected override void OnPreInitialize()
    {
        base.OnPreInitialize();
        itemDataManager = GetComponent<ItemDataManager>();
    }

    protected override void OnInitialize()
    {
        Transform child = transform.GetChild(0);
        itemPool = child.GetComponent<ItemPool>();
        itemPool?.Initialize();
    }

    /// <summary>
    /// 아이템을 생성하는 함수, 위치와 노이즈 설정 가능
    /// </summary>
    /// <param name="code">아이템 종류</param>
    /// <param name="position">생성될 위치</param>
    /// <param name="useNoise">노이즈 사용 여부</param>
    /// <returns>생성된 아이템의 게임 오브젝트</returns>
    public GameObject MakeItem(ItemCode code, Vector3? position = null, bool useNoise = false)
    {
        ItemData data = itemDataManager[code];
        ItemObject item = itemPool.GetObject();

        item.Data = data;
        item.transform.position = position.GetValueOrDefault();

        if (useNoise)
        {
            Vector3 noise = Vector3.zero;

            Vector2 rand = Random.insideUnitCircle * spawnNoise; // 반지름이 spawnNoise 인 랜덤한 위치 구하기
            noise.x = rand.x;
            noise.z = rand.y;

            item.transform.position += noise;
        }

        return item.gameObject;
    }

    /// <summary>
    /// 아이템을 여러개 생성하는 함수
    /// </summary>
    /// <param name="code">아이템의 종류</param>
    /// <param name="count">생성할 개수</param>
    /// <returns>아이템들의 게임 오브젝트 배열</returns>
    //public GameObject[] MakeItems(ItemCode code, uint count)
    //{
    //    GameObject[] items = new GameObject[count];
    //    for(int i = 0; i < count; i++)
    //    {
    //        items[i] = MakeItem(code);
    //    }
    //    return items;
    //}

    /// <summary>
    /// 아이템을 여러개 생성하는 함수, 위치와 노이즈 설정 가능
    /// </summary>
    /// <param name="code">아이템 종류</param>
    /// <param name="count">생성할 아이템의 개수</param>
    /// <param name="position">생성될 위치</param>
    /// <param name="useNoise">노이즈 사용 여부</param>
    /// <returns>생성된 아이템들의 게임 오브젝트 배열</returns>
    public GameObject[] MakeItems(ItemCode code, uint count, Vector3? position = null, bool useNoise = false)
    {
        GameObject[] items = new GameObject[count];
        for (int i = 0; i < count; i++)
        {
            items[i] = MakeItem(code, position, useNoise);
        }
        return items;
    }
}
