using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Movement", menuName = "CustomBehaviours/MovementBehaviour")]
public class MovementBehaviour : CustomBehaviour
{
    protected override void InitializeCurrentBehaviourByReceivedEntityInstance(Entity instance)
    {
    }

    public void Move()
    {
        EntityInstance.transform.Translate(Vector3.up * Time.deltaTime);
    }

    public void InitializeBehaviourInstance(Entity entity)
    {
        EntityInstance = entity;
    }
}