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

    private void RecognitionNearEntities<TEntity, TDetectingEntity, TDetectingCollider>(TEntity otherCollidedEntity,
        TDetectingEntity detectingEntity, TDetectingCollider detectingColliderName)
    {
        var currentColliderName = detectingColliderName as string;
        var otherCollider = (otherCollidedEntity as Collider);
        if (otherCollider != null && otherCollider.name == "Person")
        {
            var entity = (detectingEntity as Entity);
            if (entity != null)
            {
                switch (currentColliderName)
                {
                    case "DetectingCollider":
                        TriggerEvent(DetectingEvents.PlayerHasBeenDetected + $"by:{entity.name}", entity);
                        break;
                    case "MeleeAttackCollider":
                        TriggerEvent(DetectingEvents.PlayerEnteredTheRadiusOfMeleeAttack + $"by:{entity.name}", entity);
                        break;
                }
            }
        }
    }

    private void MissNearEntities<TEntity, TMissingEntity, TDetectingCollider>(TEntity otherCollidedEntity,
        TMissingEntity missingEntity, TDetectingCollider detectingColliderName)
    {
        var currentColliderName = detectingColliderName as string;
        var collider = (otherCollidedEntity as Collider);
        if (collider != null && collider.name == "Person")
        {
            var entity = (missingEntity as Entity);
            if (entity != null)
            {
                switch (currentColliderName)
                {
                    case "DetectingCollider":
                        TriggerEvent(DetectingEvents.PlayerHasBeenMissed + $"by:{entity.name}", entity);
                        break;
                    case "MeleeAttackCollider":
                        TriggerEvent(DetectingEvents.PlayerExitTheRadiusOfMeleeAttack + $"by:{entity.name}", entity);
                        break;
                }
            }
        }
    }


    public override void Subscribe()
    {
        ManagerEvents.StartListening($"{EntityInstance.name}{DetectingEvents.EntityDetectingColliderTriggered}",
            RecognitionNearEntities);
        ManagerEvents.StartListening($"{EntityInstance.name}{DetectingEvents.EntityDetectingColliderExit}",
            MissNearEntities);
        ManagerEvents.StartListening($"{EntityInstance.name}{MeleeAttackEvents.MeleeAttackIsOnAvailableNow}",
            RecognitionNearEntities);
        ManagerEvents.StartListening($"{EntityInstance.name}{MeleeAttackEvents.MeleeAttackIsNotAvailableNow}",
            MissNearEntities);
    }

    public override void UnSubscribe()
    {
        ManagerEvents.StopListening($"{EntityInstance.name}{DetectingEvents.EntityDetectingColliderTriggered}",
            RecognitionNearEntities);
        ManagerEvents.StopListening($"{EntityInstance.name}{DetectingEvents.EntityDetectingColliderExit}",
            MissNearEntities);
        ManagerEvents.StopListening($"{EntityInstance.name}{MeleeAttackEvents.MeleeAttackIsOnAvailableNow}",
            RecognitionNearEntities);
        ManagerEvents.StopListening($"{EntityInstance.name}{MeleeAttackEvents.MeleeAttackIsNotAvailableNow}",
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
}