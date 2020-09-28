using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monster : Interactable
{
    public MonsterState State;
    public bool isCalled = false;

    protected void Start()
    {
        State = GetComponent<MonsterIdle>();
        Idle();
    }

    protected void Update()
    {
        Action();
    }

    protected void Pursue()
    {
        State.Pursue(this);
    }

    protected void Idle()
    {
        State.Idle(this);
    }

    protected void Action()
    {
        State.DoAction(this);
    }

    public override void CauseDamage(int damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            Debug.Log("Monster was dead");
            Destroy(gameObject);
        }
    }

    public virtual void Attack(Transform attackeObject, int damage)
    {
        Debug.Log("I attack");
    }
}
