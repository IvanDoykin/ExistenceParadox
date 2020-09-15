using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterPursue : MonsterState
{
    private Monster monster;
    private MonsterIdle monsterIdle;
    private NavMeshAgent navigatorAgent;

    [SerializeField] Transform playerT;

    private void Start()
    {
        monster = GetComponent<Monster>();
        monsterIdle = GetComponent<MonsterIdle>();
        navigatorAgent = GetComponent<NavMeshAgent>();
    }

    public override void Idle(Monster monster)
    {
        Debug.Log("Set to idle");
        monster.State = monsterIdle;
    }

    public override void Pursue(Monster monster)
    {
        Debug.Log("Continue pursue");
    }

    public override void DoAction(Monster monster)
    {
        navigatorAgent.destination = playerT.position;
    }

    private void OnTriggerExit(Collider other)
    {
        if (monster.State != this)
            return;

        if (other.gameObject.GetComponent<PlayerController>() != null)
            Idle(monster);
    }
}
