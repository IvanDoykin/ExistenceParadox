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

        EntitiesDataDictionary.Add(EntityInstance, EntityInstance.entityDataDictionary);
    }

    public void Tick()
    {
        Pursue();
    }

    protected override void DeactivateCurrentInstanceModule<T>(Entity currentEntityPursueData)
    {
        EntitiesDataDictionary.Remove(currentEntityPursueData);
    }

    private void Pursue()
    {
        for (int entityNumber = 0; entityNumber < EntitiesDataDictionary.Count; entityNumber++)
        {
            ReceiveEntityInstanceData(EntitiesDataDictionary, entityNumber, "PursueData", out var receivedPursueData);
            var pursueData = (PursueData) receivedPursueData;
            pursueData.navMeshAgent.destination = pursueData.player.transform.position;
        }
    }
}