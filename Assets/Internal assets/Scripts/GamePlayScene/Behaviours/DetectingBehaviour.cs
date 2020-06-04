using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Detecting", menuName = "CustomBehaviours/Detecting")]
public class DetectingBehaviour : CustomBehaviour, IEventSub
{
    protected override void InitializeCurrentBehaviourByReceivedEntityInstance(Entity entity)
    {
        Subscribe();
    }

    private void DetectNearEntities<T>(T collider)
    {
        for (int entityNumber = 0; entityNumber < EntitiesDataDictionary.Count; entityNumber++)
        {
            ReceiveEntityInstanceData(EntitiesDataDictionary, entityNumber, "DetectingData", out var receivedData);
            var detectingData = (DetectingData) receivedData;
            var kek = (collider as Collider);
            Debug.Log(kek.name);
        }
    }

    public void Subscribe()
    {
        ManagerEvents.StartListening($"{EntityInstance.name}{DetectingEvents.EntityColliderTriggered}", DetectNearEntities);
    }

    public void UnSubscribe()
    {
    }
}