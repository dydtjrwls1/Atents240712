using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingleTon<GameManager> 
{
    Player m_Player;

    public Player Player => m_Player;

    ItemDataManager m_ItemDataManager;

    public ItemDataManager ItemData => m_ItemDataManager;

    InventoryUI m_InventoryUI;

    public InventoryUI InventoryUI => m_InventoryUI;

    protected override void OnPreInitialize()
    {
        base.OnPreInitialize();
        m_ItemDataManager = GetComponent<ItemDataManager>();
    }

    protected override void OnInitialize()
    {
        m_Player = FindAnyObjectByType<Player>();
        m_InventoryUI = FindAnyObjectByType<InventoryUI>();

        m_Player?.Initialize();
    }
}
