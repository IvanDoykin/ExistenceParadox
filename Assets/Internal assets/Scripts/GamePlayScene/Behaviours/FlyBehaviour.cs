using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Fly", menuName = "CustomBehaviours/FlyBehaviour")]
public class FlyBehaviour : CustomBehaviour, ITick, ICustomBehaviour
{
    public void Tick()
    {
        InstanceEntity.transform.Translate(Vector3.up * Time.deltaTime);
    }

    public void InitializeBehaviourInstance(Entity entity)
    {
        InstanceEntity = entity;
    }

    protected override void ReceiveAllData(Entity entity)
    {
        throw new NotImplementedException();
    }
}