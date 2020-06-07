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
        ManagerEvents.StartListening(DetectingEvents.PlayerHasBeenMissed + $"by:{EntityInstance.name}",
            StopBeingAPursuer);
    }

    public override void UnSubscribe()
    {
        ManagerEvents.StopListening(DetectingEvents.PlayerHasBeenDetected + $"by:{EntityInstance.name}",
            TakeACloserLook);
        ManagerEvents.StopListening(DetectingEvents.PlayerHasBeenMissed + $"by:{EntityInstance.name}",
            StopBeingAPursuer);
    }

    protected override void ClearModule()
    {
        _pursuers?.Clear();
        UnSubscribe();
    }

    private void TakeACloserLook<TDetectingEntity>(
        TDetectingEntity detectingEntityName)
    {
        Entity entity = detectingEntityName as Entity;
        if (entity != null) _pursuers.Add(entity);
    }

    private void StopBeingAPursuer<TMissingEntity>(TMissingEntity missingEntity)
    {
        Debug.Log("NearEntities");
        Entity entity = missingEntity as Entity;
        if (entity != null) _pursuers.Remove(entity);
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