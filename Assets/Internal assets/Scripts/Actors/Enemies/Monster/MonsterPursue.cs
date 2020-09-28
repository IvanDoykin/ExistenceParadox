using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Analytics;

[RequireComponent(typeof(MonsterIdle))]
[RequireComponent(typeof(NavMeshAgent))]

public class MonsterPursue : MonsterState
{
    public float attackSpeed;
    public bool readyForAttack = true;

    [SerializeField] private Transform pursueAim;
    [SerializeField] private float attackRange;
    [SerializeField] private int damage;

    private Monster monster;
    private MonsterIdle monsterIdle;
    private NavMeshAgent navigatorAgent;

    private void Start()
    {
        monster = GetComponent<Monster>();
        monsterIdle = GetComponent<MonsterIdle>();
        navigatorAgent = GetComponent<NavMeshAgent>();
    }

    public void SetPursueAim(Transform aim)
    {
        pursueAim = aim;
    }

    public override void Idle(Monster monster)
    {
        Debug.Log("Set to idle");
        monster.isCalled = false;
        monster.State = monsterIdle;
        GetComponent<Renderer>().material.SetColor("_BaseColor", Color.white);
    }

    public override void DoAction(Monster monster)
    {
        if ((transform.position - pursueAim.position).magnitude > attackRange)
            navigatorAgent.destination = pursueAim.position;

        else if (readyForAttack)
        {
            navigatorAgent.destination = transform.position;
            monster.Attack(pursueAim, damage);
        }
        

        Debug.Log("pursue action " + GetInstanceID());
    }

    private void OnTriggerExit(Collider other)
    {
        if (monster.State != this)
            return;

        if (other.gameObject.GetComponent<PlayerController>() != null)
        {
            Debug.Log("idle");
            Idle(monster);
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if ((monster.isCalled) || (monster.State != this))
            return;

        if (other == null)
            return;

        Monster CalledMonster = other.gameObject?.GetComponent<Monster>();

        if (CalledMonster == null)
            return;

        CalledMonster.GetComponent<MonsterPursue>().SetPursueAim(pursueAim);
        CalledMonster.GetComponent<MonsterIdle>().Pursue(CalledMonster, true);
    }
}
