using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Microsoft.Win32;
using UnityEngine;

public class Actor : MonoBehaviour, ITick, IEventTrigger
{
    public List<ScriptableObject> behavioursList;
    private int _previousBehavioursListCount;


    private void Start()
    {
        _previousBehavioursListCount = behavioursList.Count;
        SendDataToBehaviours(this);
        ManagerUpdate.AddTo(this);
    }

    public void Tick()
    {
        CheckBehavioursListCount();
    }

    private void SendDataToBehaviours(Actor actor)
    {
        if (behavioursList.Count <= 0) return;
        foreach (ScriptableObject behaviour in actor.behavioursList)
        {
            ((ICustomBehaviour) behaviour).ReceiveDataInstance(actor);
        }
    }

    private void CheckBehavioursListCount()
    {
        if (behavioursList.Count != _previousBehavioursListCount)
        {
            Debug.Log($"BehavioursListCountChanged{this.GetInstanceID()}");
            SendDataToBehaviours(this);
        }

        _previousBehavioursListCount = behavioursList.Count;
    }


    public void TriggerEvent(string eventName)
    {
        ManagerEvents.TriggerEvent(eventName);
    }
}