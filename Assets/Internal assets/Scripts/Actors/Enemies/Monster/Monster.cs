using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public MonsterState State { get; set; }

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
}
