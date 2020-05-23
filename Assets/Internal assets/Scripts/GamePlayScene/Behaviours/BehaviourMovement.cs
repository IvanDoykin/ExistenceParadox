using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BehaviourMovement", menuName = "Behaviours/BehaviourMovement")]
public class BehaviourMovement : Behaviour
{
    public void Move()
    {
        instanceActor.transform.Translate(Vector3.up * Time.deltaTime);
    }
}