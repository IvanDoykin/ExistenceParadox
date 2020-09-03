using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : Item
{
    public abstract void Attack();


    private void Start()
    {
        if (GetComponentInParent<PlayerController>())
            WeaponController.AttackEvent += Attack;
    }
    private void OnDestroy()
    {
        WeaponController.AttackEvent -= Attack;
    }
}
