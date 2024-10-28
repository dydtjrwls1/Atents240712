using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextGenerator : MonoBehaviour
{
    private void Start()
    {
        IBattle battle = GetComponentInParent<IBattle>();
        battle.onHit += GenerateHitDamage;
    }

    private void GenerateHitDamage(int damage)
    {
        Factory.Instance.GetDamageText(transform.position, damage);
    }
}

