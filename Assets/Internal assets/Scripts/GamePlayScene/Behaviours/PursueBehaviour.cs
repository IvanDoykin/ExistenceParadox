using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Packages.Rider.Editor;
using UnityEngine;
using UnityEngine.AI;
using Object = System.Object;

[CreateAssetMenu(fileName = "Pursue", menuName = "CustomBehaviours/Pursue")]
public class PursueBehaviour : CustomBehaviour, ITick
{
    private readonly Dictionary<Object, PursueData>
        _pursueDataDictionary =
            new Dictionary<Object, PursueData>(); //словарь со списком экземпляров сущности и их Pursue data

    protected override void ReceiveAllData()
    {
        PursueData pursueData = ReceivePursueData();
        _pursueDataDictionary.Add(InstanceEntity, pursueData);
    }

    protected override void DeactivateCurrentInstanceModule<T>(T currentEntityPursueData)
    {
        Debug.Log(currentEntityPursueData);
        _pursueDataDictionary.Remove(currentEntityPursueData);
    }

    private PursueData ReceivePursueData()
    {
        if (InstanceEntity.entityDataDictionary.TryGetValue("PursueData", out var receivedPursueData))
        {
            return ((PursueData) receivedPursueData);
        }

        Debug.Log($"PursueData is not found in current entity: {InstanceEntity.GetType()}");
        return null;
    }

    private void Pursue()
    {
        foreach (var pursueData in _pursueDataDictionary.Values)
        {
            pursueData.navMeshAgent.destination = pursueData.player.transform.position;
        }
    }

    public void Tick()
    {
        Pursue();
    }
}