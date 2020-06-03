using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Fly", menuName = "CustomBehaviours/FlyBehaviour")]
public class FlyBehaviour : CustomBehaviour, ITick
{
   

    protected override void InitializeCurrentBehaviourByReceivedEntityInstance(Entity instance)
    {
        throw new System.NotImplementedException();
    }

    public void Tick()
    {
        EntityInstance.transform.Translate(Vector3.up * Time.deltaTime);
    }


    protected override void ReceiveAllData()
    {
        throw new NotImplementedException();
    }

    protected override void DeactivateCurrentInstanceModule<T>(Entity argument)
    {
    }
}