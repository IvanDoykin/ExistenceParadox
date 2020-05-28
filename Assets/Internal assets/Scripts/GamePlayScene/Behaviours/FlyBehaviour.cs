using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FlyBehaviour", menuName = "CustomBehaviours/FlyBehaviour")]
public class FlyBehaviour : CustomBehaviour, ITick, ICustomBehaviour
{
    public void Tick()
    {
        InstanceEntity.transform.Translate(Vector3.up * Time.deltaTime);
    }

    public void ReceiveDataInstance(Entity entity)
    {
    }
}