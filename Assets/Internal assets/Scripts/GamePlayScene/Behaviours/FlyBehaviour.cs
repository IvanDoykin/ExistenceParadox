using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Fly", menuName = "CustomBehaviours/FlyBehaviour")]
public class FlyBehaviour : CustomBehaviour, ITick
{
    protected override void InitializeCurrentBehaviourByReceivedEntityInstance(Entity instance)
    {
    }

    public void Tick()
    {
        KekMessage();
    }


    protected override void ReceiveAllData()
    {
        if (EntityInstance.entityDataDictionary == null)
        {
            Debug.Log($"no data was found in the current entity: {EntityInstance.GetType()}");
            return;
        }

        EntitiesDataDictionary.Add(EntityInstance, EntityInstance.entityDataDictionary);
    }

    protected override void DeactivateCurrentInstanceModule<T>(Entity currentEntityPursueData)
    {
        EntitiesDataDictionary.Remove(currentEntityPursueData);
    }

    private void KekMessage()
    {
        for (int entityNumber = 0; entityNumber < EntitiesDataDictionary.Count; entityNumber++)
        {
            ReceiveEntityInstanceData(EntitiesDataDictionary, entityNumber, "PursueData", out var receivedPursueData);
            var pursueData = (PursueData) receivedPursueData;

            Debug.Log(pursueData.kek);
        }
    }
}