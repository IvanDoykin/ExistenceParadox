using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BehaviourMovement", menuName = "Behaviours/BehaviourMovement")]
public class BehaviourMovement : CustomBehaviour, ICustomBehaviour
{
    public void Move()
    {
        InstanceActor.transform.Translate(Vector3.up * Time.deltaTime);
    }

    public void ReceiveDataInstance(Actor actor)
    {
        Debug.Log(actor.GetInstanceID());
    }
}