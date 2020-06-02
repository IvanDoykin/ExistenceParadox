using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Fly", menuName = "CustomBehaviours/FlyBehaviour")]
public class FlyBehaviour : CustomBehaviour, ITick
{
    public void Tick()
    {
        InstanceEntity.transform.Translate(Vector3.up * Time.deltaTime);
    }


    protected override void ReceiveAllData()
    {
        throw new NotImplementedException();
    }

    protected override void DeactivateCurrentInstanceModule<T>(T argument)
    {
    }
}