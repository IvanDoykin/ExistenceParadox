using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "Pursue", menuName = "CustomBehaviours/Pursue")]
public class PursueBehaviour : CustomBehaviour, ICustomBehaviour, ITick
{
    public void ReceiveEntityInstance(dynamic entity)
    {
        InstanceEntity = entity;

        InstanceEntity.hpData.health = 100;
        ManagerUpdate.AddTo(this);
    }

    public void Tick()
    {
    }
}