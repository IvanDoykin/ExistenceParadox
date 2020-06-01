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
    private PursueData _pursueData;

    public void InitializeBehaviourInstance(Entity entity)
    {
        InstanceEntity = entity;
        ReceiveAllData();
        ManagerUpdate.AddTo(this);
        base.Subscribe();
        ManagerEvents.StartListening("kekchanskii", Gosue);
        ManagerEvents.TriggerEvent("kekchanskii", 10);
    }

    protected override void ReceiveAllData()
    {
        _pursueData = ReceivePursueData();
    }

    private PursueData ReceivePursueData()
    {
        if (InstanceEntity.entityDataDictionary.TryGetValue("PursueData", out dynamic receivedHpData))
        {
            return ((PursueData) receivedHpData);
        }

        Debug.Log($"PursueData is not found in current entity: {InstanceEntity.GetType()}");
        return null;
    }

    private void Pursue()
    {
        _pursueData.navMeshAgent.destination = _pursueData.player.transform.position;
    }

    private static void Gosue<T>(T argument)
    {
        Debug.Log(argument);
    }


    public void Tick()
    {
        Pursue();
    }
}