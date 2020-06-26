﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

// [CreateAssetMenu(fileName = "CustomBehaviour", menuName = "CustomBehaviour")]
public abstract class CustomBehaviour : ScriptableObject, IEventTrigger, IEventSub
{
    public abstract void TriggerEvent(string eventName, params dynamic[] arguments);
    public abstract void Subscribe();
    public abstract void UnSubscribe();

    protected abstract void ClearModule();
    protected Entity EntityInstance;

    private bool _isAlreadyUpdate = false;

    protected readonly Dictionary<Entity, Dictionary<string, Data>>
        EntitiesDataDictionary =
            new Dictionary<Entity, Dictionary<string, Data>>(); //словарь со списком экземпляров сущности со словарёи с  их data classes

    protected abstract void InitializeCurrentBehaviourByReceivedEntityInstance(Entity instance);

    public void PrimaryInitializeBehaviour(Entity currentEntity)
    {
        EntityInstance = currentEntity;
        InitializeCurrentBehaviourByReceivedEntityInstance(currentEntity);
        ReceiveAllData();
        StopListening();
        StartListening();
        AddToUpdateManager();
    }

    private void ReceiveAllData()
    {
        if (EntityInstance.entityDataDictionary == null)
        {
            Debug.Log($"no data was found in the current entity: {EntityInstance.GetType()}");
            return;
        }

        EntitiesDataDictionary.Add(EntityInstance, EntityInstance.entityDataDictionary);
    }

    private void StartListening()
    {
        ManagerEvents.StartListening($"BehavioursListChanged{EntityInstance.GetInstanceID()}",
            ShutdownCurrentInstanceModule);
    }

    private void StopListening()
    {
        ManagerEvents.StopListening($"BehavioursListChanged{EntityInstance.GetInstanceID()}",
            ShutdownCurrentInstanceModule);
    }

    private void ShutdownCurrentInstanceModule<T>(T currentEntityData)
    {
        var entityData = (currentEntityData as Entity);
        ClearModule();
        if (entityData != null)
            EntitiesDataDictionary.Remove(entityData);
    }

    private void AddToUpdateManager()
    {
        if (_isAlreadyUpdate)
            return;
        ManagerUpdate.AddTo(this);
    }
}