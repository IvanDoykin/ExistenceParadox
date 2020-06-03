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