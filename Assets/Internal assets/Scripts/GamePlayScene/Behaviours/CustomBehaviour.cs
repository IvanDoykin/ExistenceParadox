using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// [CreateAssetMenu(fileName = "CustomBehaviour", menuName = "CustomBehaviour")]
public abstract class CustomBehaviour : ScriptableObject, IEventSub
{
    protected Entity InstanceEntity;

    public void StartListening(string eventName, UnityAction listener)
    {
        ManagerEvents.StartListening(eventName, listener);
    }

    public void StopListening(string eventName, UnityAction listener)
    {
        ManagerEvents.StopListening(eventName, listener);
    }

    public void Subscribe()
    {
        StartListening($"BehavioursListChanged{InstanceEntity.GetInstanceID()}", DeactivateCurrentInstanceModule);
    }

    public void UnSubscribe()
    {
        StopListening($"BehavioursListChanged{InstanceEntity.GetInstanceID()}", DeactivateCurrentInstanceModule);
    }

    public void Kek()
    {
        Debug.Log(GetInstanceID());
    }

    protected abstract void ReceiveAllData();

    private void DeactivateCurrentInstanceModule()
    {
        Debug.Log(GetInstanceID());
        UnSubscribe();
        ManagerUpdate.RemoveFrom(this);
    }
}