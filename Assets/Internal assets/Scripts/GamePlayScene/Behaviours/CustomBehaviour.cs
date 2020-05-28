using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// [CreateAssetMenu(fileName = "CustomBehaviour", menuName = "CustomBehaviour")]
public class CustomBehaviour : ScriptableObject, IEventSub
{
    protected Entity InstanceEntity;

    public void ReceiveActor(Entity entity)
    {
        this.InstanceEntity = entity;
        Debug.Log(InstanceEntity.GetInstanceID());
    }


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
    }

    public void UnSubscribe()
    {
    }
}