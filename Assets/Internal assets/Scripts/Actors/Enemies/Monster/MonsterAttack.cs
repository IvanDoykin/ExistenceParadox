using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonsterState
{
    MonsterPursue monsterPursue;

    private void Start()
    {
        monsterPursue = GetComponent<MonsterPursue>();
    }

    public override void DoAction(Monster monster)
    {
        Debug.LogError("ATTACK");
    }

    public override void Pursue(Monster monster, bool call = false)
    {
        Debug.Log("Monster is pursuing");
        monster.State = monsterPursue;
    }
}
