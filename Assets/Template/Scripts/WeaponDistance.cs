using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDistance : Weapon
{

    public override void Attack()
    {
        Debug.Log("ffff");
    }

    public override object m_GetType()
    {
        return this;
    }
}
