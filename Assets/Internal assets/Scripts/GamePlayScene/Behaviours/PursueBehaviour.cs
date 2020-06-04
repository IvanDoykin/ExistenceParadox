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

    public void Tick()
    {
        Pursue();
    }

    private void Pursue()
    {
        for (int entityNumber = 0; entityNumber < EntitiesDataDictionary.Count; entityNumber++)
        {
            ReceiveEntityInstanceData(EntitiesDataDictionary, entityNumber, "PursueData", out var receivedData);
            var pursueData = (PursueData) receivedData;
            pursueData.navMeshAgent.destination = pursueData.player.transform.position;
        }
    }
}