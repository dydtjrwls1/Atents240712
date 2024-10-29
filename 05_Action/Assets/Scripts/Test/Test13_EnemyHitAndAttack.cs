using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test13_EnemyHitAndAttack : TestBase
{
    public Enemy slime;
    public float damage = 30.0f;

    EnemyHealth health;
    EnemyBattle battle;

    private void Start()
    {
        Player player = GameManager.Instance.Player;
        player.InventoryData.AddItem(ItemCode.IronSword);
        player.PlayerInventory.EquipItem(EquipType.Weapon, player.InventoryData[0]);

        PlayerMovement movement = player.GetComponent<PlayerMovement>();
        movement.ToggleMoveMode();

        battle = slime.GetComponent<EnemyBattle>();
        health = slime.GetComponent<EnemyHealth>(); 
    }

    protected override void Test1_performed(InputAction.CallbackContext context)
    {
        health.GetDamage(50.0f);
    }

    protected override void Test2_performed(InputAction.CallbackContext context)
    {
        health.HealthHeal(10.0f);
    }

    protected override void Test3_performed(InputAction.CallbackContext context)
    {
        battle.Defence(damage);
    }
}
