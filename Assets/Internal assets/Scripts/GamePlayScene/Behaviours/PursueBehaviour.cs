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
    private readonly Dictionary<Entity, PursueData>
        _pursueDataDictionary =
            new Dictionary<Entity, PursueData>(); //словарь со списком экземпляров сущности и их Pursue data

    protected override void InitializeCurrentBehaviourByReceivedEntityInstance(Entity instance)
    {
    }

    protected override void ReceiveAllData()
    {
        PursueData pursueData = ReceivePursueData();
        _pursueDataDictionary.Add(EntityInstance, pursueData);
    }

    protected override void DeactivateCurrentInstanceModule<T>(Entity currentEntityPursueData)
    {
        Debug.Log(currentEntityPursueData);
        _pursueDataDictionary.Remove(currentEntityPursueData);
    }

    private PursueData ReceivePursueData()
    {
        if (EntityInstance.entityDataDictionary.TryGetValue("PursueData", out var receivedPursueData))
        {
            return ((PursueData) receivedPursueData);
        }

        Debug.Log($"PursueData is not found in current entity: {EntityInstance.GetType()}");
        return null;
    }

    private void Pursue()
    {
        for (int number = 0; number < _pursueDataDictionary.Count; number++)
        {
            Entity entity = _pursueDataDictionary.ElementAt(number).Key;
            PursueData pursueData = _pursueDataDictionary.ElementAt(number).Value;
            pursueData.navMeshAgent.destination = pursueData.player.transform.position;
        }
    }

    public void Tick()
    {
        Pursue();
    }
}