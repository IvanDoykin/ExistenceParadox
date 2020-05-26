using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CustomBehaviourMovement", menuName = "Behaviours/CustomBehaviourMovement")]
public class CustomBehaviourMovement : CustomBehaviour, ICustomBehaviour
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