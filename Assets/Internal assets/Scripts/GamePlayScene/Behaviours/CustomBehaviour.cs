using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// [CreateAssetMenu(fileName = "CustomBehaviour", menuName = "CustomBehaviour")]
public abstract class CustomBehaviour : ScriptableObject, IEventSub
{
    protected Entity InstanceEntity;
    protected abstract void ReceiveAllData();

    public void Subscribe()
    {
        ManagerEvents.StartListening($"BehavioursListChanged{InstanceEntity.GetInstanceID()}",
            DeactivateCurrentInstanceModule);
    }

    public void UnSubscribe()
    {
        ManagerEvents.StopListening($"BehavioursListChanged{InstanceEntity.GetInstanceID()}",
            DeactivateCurrentInstanceModule);
    }


    private void DeactivateCurrentInstanceModule()
    {
        UnSubscribe();
        ManagerUpdate.RemoveFrom(this);
    }
}