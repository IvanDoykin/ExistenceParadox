using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.AssetImporters;
using UnityEngine;
using Object = System.Object;

[CreateAssetMenu(fileName = "Detecting", menuName = "CustomBehaviours/Detecting")]
public class DetectingBehaviour : CustomBehaviour, IState
{
    protected override void InitializeCurrentBehaviourByReceivedEntityInstance(Entity entity)
    {
        Subscribe();
    }

    private void DetectNearEntities<T, TDetectingEntityName>(T collidedEntity, TDetectingEntityName detectingEntity)
    {
        var collider = (collidedEntity as Collider);
        if (collider != null && collider.name == "Person")
        {
            var entity = (detectingEntity as Entity);
            if (entity != null)
            {
                TriggerEvent(DetectingEvents.PlayerHasBeenDetected + $"by:{entity.name}", entity);
                ChangeCurrentState(entity);
            }
        }
    }


    public override void Subscribe()
    {
        ManagerEvents.StartListening($"{EntityInstance.name}{DetectingEvents.EntityColliderTriggered}",
            DetectNearEntities);
    }

    public override void UnSubscribe()
    {
    }

    public override void TriggerEvent(string eventName, params Object[] arguments)
    {
        ManagerEvents.CheckTriggeringEvent(eventName, arguments);
    }

    public void ChangeCurrentState(Entity entity)
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
}