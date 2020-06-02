using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// [CreateAssetMenu(fileName = "CustomBehaviour", menuName = "CustomBehaviour")]
public abstract class CustomBehaviour : ScriptableObject
{
    protected Entity EntityInstance;
    private bool _isAlreadyUpdate = false;
    protected abstract void ReceiveAllData();
    protected abstract void DeactivateCurrentInstanceModule<T>(Entity argument);
    protected abstract void InitializeCurrentBehaviourByReceivedEntityInstance(Entity instance);

    public void PrimaryInitializeBehaviour(Entity currentEntity)
    {
        EntityInstance = currentEntity;
        ReceiveAllData();
        UnSubscribe();
        Subscribe();
        AddToUpdateManager();
    }

    private void Subscribe()
    {
        ManagerEvents.StartListening($"BehavioursListChanged{EntityInstance.GetInstanceID()}",
            argument => DeactivateCurrentInstanceModule<object>(argument));
    }

    private void UnSubscribe()
    {
        ManagerEvents.StopListening($"BehavioursListChanged{EntityInstance.GetInstanceID()}",
            argument => DeactivateCurrentInstanceModule<object>(argument));
    }

    private void AddToUpdateManager()
    {
        if (_isAlreadyUpdate)
            return;
        ManagerUpdate.AddTo(this);
    }
}