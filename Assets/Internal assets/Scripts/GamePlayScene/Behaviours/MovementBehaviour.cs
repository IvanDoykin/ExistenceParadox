using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Movement", menuName = "CustomBehaviours/MovementBehaviour")]
public class MovementBehaviour : CustomBehaviour
{
    protected override void ReceiveEntityInstanceData(Dictionary<Entity, Dictionary<string, Data>> dataDictionary, int entityNumber, string typeName, out Data currentData)
    {
        throw new NotImplementedException();
    }

    protected override void InitializeCurrentBehaviourByReceivedEntityInstance(Entity instance)
    {
        throw new System.NotImplementedException();
    }

    public void Move()
    {
        EntityInstance.transform.Translate(Vector3.up * Time.deltaTime);
    }

    public void InitializeBehaviourInstance(Entity entity)
    {
        EntityInstance = entity;
    }

    protected override void ReceiveAllData()
    {
        throw new NotImplementedException();
    }

    protected override void DeactivateCurrentInstanceModule<T>(Entity argument)
    {
    }
}