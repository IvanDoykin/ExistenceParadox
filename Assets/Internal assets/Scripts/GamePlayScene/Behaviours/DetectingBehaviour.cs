using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.AssetImporters;
using UnityEngine;

[CreateAssetMenu(fileName = "Detecting", menuName = "CustomBehaviours/Detecting")]
public class DetectingBehaviour : CustomBehaviour, IEventSub, IEventTrigger, IArgumentativeEventTrigger
{
    protected override void InitializeCurrentBehaviourByReceivedEntityInstance(Entity entity)
    {
        Subscribe();
    }

    private void DetectNearEntities<T, TDetectingEntityName>(T collidedEntity, TDetectingEntityName detectingEntityName)
    {
        var collider = (collidedEntity as Collider);
        // if (collider != null && collider.name == "Player")
    }

    public void Subscribe()
    {
        ManagerEvents.StartListening($"{EntityInstance.name}{DetectingEvents.EntityColliderTriggered}",
            DetectNearEntities);
    }

    public void UnSubscribe()
    {
    }

    public void TriggerEvent(string eventName)
    {
    }

    public void TriggerEvent(string eventName, params dynamic[] arguments)
    {
    }
}