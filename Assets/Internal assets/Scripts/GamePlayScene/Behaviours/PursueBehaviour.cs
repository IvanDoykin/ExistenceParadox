using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Packages.Rider.Editor;
using UnityEngine;
using UnityEngine.AI;
using Object = System.Object;

// ReSharper disable CheckNamespace

[CreateAssetMenu(fileName = "Pursue", menuName = "CustomBehaviours/Pursue")]
public class PursueBehaviour : CustomBehaviour, ITick
{
    private readonly List<PursueDataCash> pursuers = new List<PursueDataCash>();

    protected override void InitializeCurrentBehaviourByReceivedEntityInstance(Entity instance)
    {
        Subscribe();

        PursueDataCash pursueDataCash = new PursueDataCash(instance);
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
        pursuers?.Clear();
        UnSubscribe();
    }

    private void TakeACloserLook<TDetectingEntity>(
        TDetectingEntity detectingEntity)
    {
        Entity entity = detectingEntity as Entity;
        if (entity != null)
        {
            pursuers.Add(new PursueDataCash(entity));
            ChangeStateToPursuingAPlayer(entity);
        }
    }

    private void StopBeingAPursuer<TMissingEntity>(TMissingEntity missingEntity)
    {
        Entity entity = missingEntity as Entity;

        for (int pursuerNumber = 0; pursuerNumber < pursuers.Count; pursuerNumber++)
        {
            if (entity != null && pursuers[pursuerNumber].entity.name == entity.name)
            {
                pursuers.Remove(pursuers[pursuerNumber]);
            }
        }

        if (entity != null)
        {
            ChangeStateToRelax(entity);
        }
    }

    private void Pursue()
    {
        foreach (PursueDataCash pursuer in pursuers)
        {
            if (pursuer.pursueData != null && pursuer.pursueData.IsDisabled != true)
            {
                pursuer.pursueData.navMeshAgent.destination = pursuer.pursueData.player.transform.position;
            }
        }
    }

    private void ChangeStateToPursuingAPlayer(Entity entity)
    {
        if (EntitiesDataDictionary.TryGetValue(entity, out Dictionary<string, Data> pursuerEntity))
        {
            for (int componentNumber = 0; componentNumber < pursuerEntity.Count; componentNumber++)
            {
                pursuerEntity.Values.ElementAt(componentNumber).IsDisabled =
                    pursuerEntity.Keys.ElementAt(componentNumber) != "PursueData";
            }
        }

        entity.currentState = $"{entity.name}: Pursuing a player";
    }

    private void ChangeStateToRelax(Entity entity)
    {
        if (EntitiesDataDictionary.TryGetValue(entity, out Dictionary<string, Data> pursuerEntity))
        {
            for (int componentNumber = 0; componentNumber < pursuerEntity.Count; componentNumber++)
            {
                pursuerEntity.Values.ElementAt(componentNumber).IsDisabled =
                    pursuerEntity.Keys.ElementAt(componentNumber) == "PursueData";
            }
        }

        entity.currentState = $"{entity.name}: Relaxed";
    }
}