using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Packages.Rider.Editor;
using UnityEngine;
using UnityEngine.AI;
using Object = System.Object;

[CreateAssetMenu(fileName = "Pursue", menuName = "CustomBehaviours/Pursue")]
public class PursueBehaviour : CustomBehaviour, ITick
{
    private readonly Dictionary<Entity, Dictionary<string, Data>>
        _entitiesDataDictionary =
            new Dictionary<Entity, Dictionary<string, Data>>(); //словарь со списком экземпляров сущности и их Pursue data

    protected override void InitializeCurrentBehaviourByReceivedEntityInstance(Entity instance)
    {
    }

    protected override void ReceiveAllData()
    {
        if (EntityInstance.entityDataDictionary == null)
        {
            Debug.Log($"no data was found in the current entity: {EntityInstance.GetType()}");
            return;
        }

        _entitiesDataDictionary.Add(EntityInstance, EntityInstance.entityDataDictionary);
    }

    protected override void DeactivateCurrentInstanceModule<T>(Entity currentEntityPursueData)
    {
        _entitiesDataDictionary.Remove(currentEntityPursueData);
    }

    private void Pursue()
    {
        for (int entityNumber = 0; entityNumber < _entitiesDataDictionary.Count; entityNumber++)
        {
            ReceiveEntityInstanceData(_entitiesDataDictionary, entityNumber, "PursueData", out var receivedPursueData1);
            var pursueData = (PursueData) receivedPursueData1;
            pursueData.navMeshAgent.destination = pursueData.player.transform.position;
        }
    }

    public void Tick()
    {
        Pursue();
    }
}