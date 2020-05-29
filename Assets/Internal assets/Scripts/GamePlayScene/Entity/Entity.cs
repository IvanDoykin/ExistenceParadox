﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.InteropServices;
using Malee.List;
using Microsoft.Win32;
using TopAdventure.Unity;
using UnityEngine;

public abstract class Entity : MonoBehaviour, ITick, IEventTrigger
{
    [Reorderable] public CustomBehavioursList behavioursList;

    private int _previousBehavioursListCount;

    private readonly CustomBehavioursList _previousBehavioursList = new CustomBehavioursList();

    public delegate void ListState();

    public event ListState BehavioursListChanged;

    public Dictionary<string, Data> entityDataDictionary = new Dictionary<string, Data>();


    [System.Serializable]
    public class CustomBehavioursList : ReorderableArray<CustomBehaviour>
    {
    }


    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        BehavioursListChanged += SendEntityInstanceToBehaviours;
        FillingPreviousBehavioursList();
        _previousBehavioursListCount = behavioursList.Count;
        SendEntityInstanceToBehaviours();
        ManagerUpdate.AddTo(this);
    }

    public void Tick()
    {
        CheckBehavioursListState();
    }

    private void SendEntityInstanceToBehaviours()
    {
        foreach (var behaviour in behavioursList)
        {
            if (behaviour == null)
            {
                Debug.Log("Please set all specific behaviour to behavioursList");
                return;
            }

            ((ICustomBehaviour) behaviour).ReceiveEntityInstance(this);
        }
    }


    private void CheckBehavioursListState()
    {
        bool isBehavioursCountChanged = false;
        bool isBehavioursValuesChanged = false;

        CheckBehavioursListCount(ref isBehavioursCountChanged);
        CheckBehavioursListValues(ref isBehavioursValuesChanged, isBehavioursCountChanged);
        if (isBehavioursCountChanged || isBehavioursValuesChanged)
            BehavioursListChanged?.Invoke();
    }

    private void CheckBehavioursListCount(ref bool isCountChanged)
    {
        if (behavioursList.Count <= 0)
            return;

        if (behavioursList.Count != _previousBehavioursListCount)
        {
            isCountChanged = true;
        }

        _previousBehavioursListCount = behavioursList.Count;
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


    public void TriggerEvent(string eventName)
    {
        ManagerEvents.TriggerEvent(eventName);
    }
}