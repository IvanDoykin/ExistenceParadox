using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// [CreateAssetMenu(fileName = "CustomBehaviour", menuName = "CustomBehaviour")]
public abstract class CustomBehaviour : ScriptableObject, IEventSub
{
    protected Entity InstanceEntity;
    private bool _isAlreadyUpdate = false;
    protected abstract void ReceiveAllData();
    protected abstract void DeactivateCurrentInstanceModule<T>(T argument);

    public void InitializeBehaviourInstance(Entity currentEntity)
    {
        InstanceEntity = currentEntity;
        ReceiveAllData();
        UnSubscribe();
        Subscribe();
        AddToUpdateManager();
    }

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

    private void AddToUpdateManager()
    {
        if (_isAlreadyUpdate)
            return;
        ManagerUpdate.AddTo(this);
    }
}