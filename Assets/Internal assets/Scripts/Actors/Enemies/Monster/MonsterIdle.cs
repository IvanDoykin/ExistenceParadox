using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterIdle : MonsterState
{
    private Monster monster;
    private MonsterPursue monsterPursue;

    private void Start()
    {
        monster = GetComponent<Monster>();
        monsterPursue = GetComponent<MonsterPursue>();
    }

    public override void Idle(Monster monster)
    {
        Debug.Log("Continue idle");
    }

    public override void Pursue(Monster monster)
    {
        Debug.Log("Start pursue");
        monster.State = monsterPursue;
    }

    public override void DoAction(Monster monster)
    {
        //nothing
    }

    private void OnTriggerEnter(Collider other)
    {
        if (monster.State != this)
            return;

        if (other.gameObject.GetComponent<PlayerController>() != null)
            Pursue(monster);
    }
}
