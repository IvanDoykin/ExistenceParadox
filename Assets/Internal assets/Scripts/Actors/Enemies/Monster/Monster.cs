using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Interactable
{
    public MonsterState State;
    public bool isCalled = false;

    private void Start()
    {
        State = GetComponent<MonsterIdle>();
        Idle();
    }

    private void Update()
    {
        Action();
    }

    private void Pursue()
    {
        State.Pursue(this);
    }

    private void Idle()
    {
        State.Idle(this);
    }

    private void Action()
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
}
