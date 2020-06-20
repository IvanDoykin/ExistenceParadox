using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.InteropServices;
using Malee.List;
using Microsoft.Win32;
using TopAdventure.Unity;
using UnityEngine;
using Object = System.Object;

public abstract class Entity : MonoBehaviour, ITick, IEventTrigger
{
    public string currentState;
    [Reorderable] public CustomBehavioursList behavioursList;

    private int _previousBehavioursListCount;

    private readonly CustomBehavioursList _previousBehavioursList = new CustomBehavioursList();

    public Dictionary<string, Data> entityDataDictionary = new Dictionary<string, Data>();

    public delegate void ListState(Entity currentEntity);

    public event ListState BehavioursListChanged;

    [System.Serializable]
    public class CustomBehavioursList : ReorderableArray<CustomBehaviour>
    {
    }

    protected void Initialize()
    {
        BehavioursListChanged += SendEntityInstanceToBehaviours;
        FillingPreviousBehavioursList();
        _previousBehavioursListCount = behavioursList.Count;
        SendEntityInstanceToBehaviours(this);
        ManagerUpdate.AddTo(this);
    }

    public void Tick()
    {
        CheckBehavioursListState();
    }

    private void SendEntityInstanceToBehaviours(Entity currentEntity)
    {
        TriggerEvent($"BehavioursListChanged{currentEntity.GetInstanceID()}", currentEntity);
        foreach (CustomBehaviour behaviour in behavioursList)
        {
            if (behaviour == null)
            {
                Debug.Log("Please set all specific behaviour to behavioursList");
                return;
            }

            behaviour.PrimaryInitializeBehaviour(currentEntity);
        }
    }

    protected void WriteCollectedData(params Data[] dataVariables)
    {
        entityDataDictionary.Clear();
        foreach (var currentData in dataVariables)
        {
            entityDataDictionary.Add(currentData.GetType().ToString(), currentData);
        }
    }


    private void CheckBehavioursListState()
    {
        bool isBehavioursCountChanged = false;
        bool isBehavioursValuesChanged = false;

        CheckBehavioursListCount(ref isBehavioursCountChanged);
        CheckBehavioursListValues(ref isBehavioursValuesChanged, isBehavioursCountChanged);
        if (isBehavioursCountChanged || isBehavioursValuesChanged)
            BehavioursListChanged?.Invoke(this);
    }

    private void CheckBehavioursListCount(ref bool isCountChanged)
    {
        if (behavioursList.Count != _previousBehavioursListCount)
        {
            isCountChanged = true;
            _previousBehavioursListCount = behavioursList.Count;
        }
    }

    private void CheckBehavioursListValues(ref bool isValuesChanged, bool isCountChanged)
    {
        if (isCountChanged.Equals(false) && behavioursList.Count > 0)
            for (int behaviourNumber = 0; behaviourNumber < _previousBehavioursList.Count; behaviourNumber++)
            {
                if (_previousBehavioursList[behaviourNumber] != behavioursList[behaviourNumber])
                {
                    isValuesChanged = true;
                    break;
                }
            }

        FillingPreviousBehavioursList();
    }

    private void FillingPreviousBehavioursList()
    {
        _previousBehavioursList.Clear();

        foreach (var behaviour in behavioursList)
        {
            _previousBehavioursList.Add(behaviour);
        }
    }


    public void TriggerEvent(string eventName, params Object[] arguments)
    {
        ManagerEvents.CheckTriggeringEvent(eventName, arguments[0]);
    }
}