using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Fly", menuName = "CustomBehaviours/FlyBehaviour")]
public class FlyBehaviour : CustomBehaviour, ITick, ICustomBehaviour
{
    public void Tick()
    {
        InstanceEntity.transform.Translate(Vector3.up * Time.deltaTime);
    }

    public void ReceiveEntityInstance(Entity gameObject)
    {
        InstanceEntity = gameObject;
    }
    
}