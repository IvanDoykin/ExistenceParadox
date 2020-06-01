using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Packages.Rider.Editor;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "Pursue", menuName = "CustomBehaviours/Pursue")]
public class PursueBehaviour : CustomBehaviour, ICustomBehaviour, ITick
{
    private readonly List<PursueData> _pursueDataList = new List<PursueData>();

    public void InitializeBehaviourInstance(Entity entity)
    {
        InstanceEntity = entity;
        ReceiveAllData();
        base.Subscribe();
        ManagerUpdate.AddTo(this);
    }

    protected override void ReceiveAllData()
    {
        var pursueData = ReceivePursueData();
        _pursueDataList.Add(pursueData);
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
        foreach (var pursueData in _pursueDataList)
        {
            pursueData.navMeshAgent.destination = pursueData.player.transform.position;
        }
    }

    public void Tick()
    {
        Pursue();
        foreach (var pursueData in _pursueDataList)
        {
            Debug.Log(pursueData.kek);
        }
    }
}