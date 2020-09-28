using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MonsterPursue))]

public class MeleeMonster : Monster
{
    public override void Attack(Transform attackedObject, int damage)
    {
        GetComponent<MonsterPursue>().readyForAttack = false;
        GetComponent<Renderer>().material.SetColor("_BaseColor", Color.black);

        attackedObject.GetComponent<Interactable>()?.CauseDamage(damage);
        StartCoroutine(AttackCooldown());
    }

    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(60f / GetComponent<MonsterPursue>().attackSpeed);
        GetComponent<MonsterPursue>().readyForAttack = true;
        GetComponent<Renderer>().material.SetColor("_BaseColor", Color.red);
    }
}
