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
    readonly List<Entity> _pursuers = new List<Entity>();

    protected override void InitializeCurrentBehaviourByReceivedEntityInstance(Entity instance)
    {
        _pursuers?.Clear();
        Subscribe();
    }

    public void Tick()
    {
        Pursue();
    }

    public override void TriggerEvent(string eventName, params dynamic[] arguments)
    {
    }

    public override void Subscribe()
    {
        ManagerEvents.StartListening(DetectingEvents.PlayerHasBeenDetected + $"by:{EntityInstance.name}",
            TakeACloserLook);
    }

    public override void UnSubscribe()
    {
    }

    private void TakeACloserLook<TDetectingEntityName>(
        TDetectingEntityName detectingEntityName)
    {
        Debug.Log($"playerHasBeenDetected by:{detectingEntityName}");
        Entity entity = detectingEntityName as Entity;
        if (entity != null) _pursuers.Add(entity);
    }


    private void Pursue()
    {
        foreach (var pursuer in _pursuers)
        {
            if (EntitiesDataDictionary.TryGetValue(pursuer, out Dictionary<string, Data> pursuerEntity))
            {
                pursuerEntity.TryGetValue("PursueData", out var receivedData);
                var pursueData = (PursueData) receivedData;
                if (pursueData != null && pursueData.IsDisabled != true)
                {
                    pursueData.navMeshAgent.destination = pursueData.player.transform.position;
                }
            }
        }
    }
}