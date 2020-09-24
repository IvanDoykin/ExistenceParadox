using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterPursue : MonsterState
{
    public delegate void SetPursueAimEvents(Transform pursueAim);
    public static event SetPursueAimEvents SetPursueAimCall;

    public delegate void PursueEvents(Monster monster, bool isCalled);
    public static event PursueEvents PursueCall;

    [SerializeField] private Transform pursueAim;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackSpeed;
    [SerializeField] private int damage;

    private bool readyForAttack = true;
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
        monster.State = monsterIdle;
        GetComponent<Renderer>().material.SetColor("_BaseColor", Color.white);
    }

    public override void DoAction(Monster monster)
    {
        navigatorAgent.destination = pursueAim.position;

        Debug.Log("pursue action " + GetInstanceID());

        if (((transform.position - pursueAim.position).magnitude <= attackRange) && readyForAttack)
            Attack(pursueAim);
    }

    private void Attack(Transform attackedObject)
    {
        readyForAttack = false;
        GetComponent<Renderer>().material.SetColor("_BaseColor", Color.black);

        attackedObject.GetComponent<Interactable>()?.CauseDamage(damage);
        StartCoroutine(AttackCooldown());
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

    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(60f / attackSpeed);
        readyForAttack = true;
        GetComponent<Renderer>().material.SetColor("_BaseColor", Color.red);
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

        SetPursueAimCall += CalledMonster.GetComponent<MonsterPursue>().SetPursueAim;
        SetPursueAimCall(pursueAim);
        SetPursueAimCall -= CalledMonster.GetComponent<MonsterPursue>().SetPursueAim;

        PursueCall += CalledMonster.GetComponent<MonsterIdle>().Pursue;
        PursueCall(CalledMonster, true);
        PursueCall -= CalledMonster.GetComponent<MonsterIdle>().Pursue;
    }
}
