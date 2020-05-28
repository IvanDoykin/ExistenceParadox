using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MovementBehaviour", menuName = "CustomBehaviours/MovementBehaviour")]
public class MovementBehaviour : CustomBehaviour, ICustomBehaviour
{
    public void Move()
    {
        InstanceEntity.transform.Translate(Vector3.up * Time.deltaTime);
    }

    public void ReceiveDataInstance(Entity entity)
    {
        Debug.Log(entity.GetInstanceID());
    }
}