using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PursueBehaviour : CustomBehaviour, ICustomBehaviour, ITick
{
    public void ReceiveEntityInstance(Entity gameObject)
    {
        // InstanceData = 
        InstanceEntity = gameObject;

        ManagerUpdate.AddTo(this);
    }

    public void Tick()
    {
    }
}