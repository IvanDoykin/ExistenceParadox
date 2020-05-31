using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Movement", menuName = "CustomBehaviours/MovementBehaviour")]
public class MovementBehaviour : CustomBehaviour, ICustomBehaviour
{
    public void Move()
    {
        InstanceEntity.transform.Translate(Vector3.up * Time.deltaTime);
    }

    public void InitializeBehaviourInstance(Entity entity)
    {
        InstanceEntity = entity;
    }

    protected override void ReceiveAllData()
    {
        throw new NotImplementedException();
    }
}