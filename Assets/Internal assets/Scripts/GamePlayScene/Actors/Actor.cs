using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Malee.List;
using Microsoft.Win32;
using TopAdventure.Unity;
using UnityEngine;

public class Actor : MonoBehaviour, ITick, IEventTrigger
{
    private int _previousBehavioursListCount;

    [Reorderable] public ReorderableBehavioursList behavioursList;
    
    [System.Serializable]
    public class ReorderableBehavioursList : ReorderableArray<CustomBehaviour>
    {
        
    }

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
        foreach (var behaviour in actor.behavioursList)
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