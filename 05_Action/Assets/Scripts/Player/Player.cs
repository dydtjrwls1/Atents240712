using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInputController))]
[RequireComponent(typeof(PlayerMovement), typeof(PlayerAttack), typeof(PlayerInventory))]
public class Player : MonoBehaviour
{
    CharacterController m_CharacterController;

    PlayerInputController m_PlayerInputController;

    PlayerMovement m_PlayerMovement;

    PlayerAttack m_PlayerAttack;

    PlayerInventory m_PlayerInventory;

    public Inventory InventoryData => m_PlayerInventory.Inventory;

    public float ItemPickUpRange => m_PlayerInventory.pickUpRange;

    public PlayerInventory PlayerInventory => m_PlayerInventory;

        
    private void Awake()
    {
        m_CharacterController = GetComponent<CharacterController>();

        m_PlayerInputController = GetComponent<PlayerInputController>();

        m_PlayerMovement = GetComponent<PlayerMovement>();
       

        m_PlayerAttack = GetComponent<PlayerAttack>();
        m_PlayerInputController.onAttack += m_PlayerAttack.OnAttackInput;

        m_PlayerInventory = GetComponent<PlayerInventory>();

        m_PlayerInputController.onMove += m_PlayerMovement.SetDirection;
        m_PlayerInputController.onMoveModeChange += m_PlayerMovement.ToggleMoveMode;
        m_PlayerInputController.onPickUp += m_PlayerInventory.PickUpItems;
    }

    // 초기화 순서를 정해서 꼬이는 일이 없게하기 위함
    public void Initialize()
    {
        IInitializable[] inits = GetComponents<IInitializable>();
        foreach(var init in inits)
        {
            init.Initialize();
        }
    }
}
