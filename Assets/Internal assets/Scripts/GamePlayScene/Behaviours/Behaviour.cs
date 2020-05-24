using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Behaviour", menuName = "Behaviour")]
public class Behaviour : ScriptableObject, IEventSub
{
    public EventsCollection actorBehaviourInitialized;
    public Actor instanceActor;

    // private void Awake()
    // {
    //     Subscribe();
    // }

    public void ReceiveData(Data data)
    {
    }

    public void ReceiveActor(Actor actor)
    {
        this.instanceActor = actor;
        Debug.Log(instanceActor.GetInstanceID());
    }

    protected virtual void Initialize()
    {
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
        StartListening(actorBehaviourInitialized.currentEvent, Initialize);
    }

    public void UnSubscribe()
    {
        StopListening(actorBehaviourInitialized.currentEvent, Initialize);
    }
}