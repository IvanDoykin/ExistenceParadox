using System.Collections;
using System.Collections.Generic;
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

    private void DetectNearEntities<T, TDetectingEntityName>(T collidedEntity, TDetectingEntityName detectingEntityName)
    {
        var collider = (collidedEntity as Collider);
        if (collider != null && collider.name == "Person")
            TriggerEvent(DetectingEvents.PlayerHasBeenDetected + $"by:{detectingEntityName}", detectingEntityName);
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
}