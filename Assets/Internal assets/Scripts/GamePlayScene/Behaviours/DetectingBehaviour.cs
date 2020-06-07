using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.AssetImporters;
using UnityEngine;
using Object = System.Object;

[CreateAssetMenu(fileName = "Detecting", menuName = "CustomBehaviours/Detecting")]
public class DetectingBehaviour : CustomBehaviour
{
    protected override void InitializeCurrentBehaviourByReceivedEntityInstance(Entity entity)
    {
        Subscribe();
    }

    private void RecognitionNearEntities<TEntity, TDetectingEntity>(TEntity otherCollidedEntity,
        TDetectingEntity detectingEntity)
    {
        var collider = (otherCollidedEntity as Collider);
        if (collider != null && collider.name == "Person")
        {
            var entity = (detectingEntity as Entity);
            if (entity != null)
            {
                TriggerEvent(DetectingEvents.PlayerHasBeenDetected + $"by:{entity.name}", entity);
                ChangeStateToPursuingAPlayer(entity);
            }
        }
    }

    private void MissNearEntities<TEntity, TMissingEntity>(TEntity otherCollidedEntity,
        TMissingEntity missingEntity)
    {
        var collider = (otherCollidedEntity as Collider);
        if (collider != null && collider.name == "Person")
        {
            var entity = (missingEntity as Entity);
            if (entity != null)
            {
                TriggerEvent(DetectingEvents.PlayerHasBeenMissed + $"by:{entity.name}", entity);
                ChangeStateToRelax(entity);
            }
        }
    }


    public override void Subscribe()
    {
        ManagerEvents.StartListening($"{EntityInstance.name}{DetectingEvents.EntityColliderTriggered}",
            RecognitionNearEntities);
        ManagerEvents.StartListening($"{EntityInstance.name}{DetectingEvents.EntityColliderExit}",
            MissNearEntities);
    }

    public override void UnSubscribe()
    {
        ManagerEvents.StopListening($"{EntityInstance.name}{DetectingEvents.EntityColliderTriggered}",
            RecognitionNearEntities);
        ManagerEvents.StopListening($"{EntityInstance.name}{DetectingEvents.EntityColliderExit}",
            MissNearEntities);
    }

    protected override void ClearModule()
    {
        UnSubscribe();
    }

    public override void TriggerEvent(string eventName, params Object[] arguments)
    {
        ManagerEvents.CheckTriggeringEvent(eventName, arguments);
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