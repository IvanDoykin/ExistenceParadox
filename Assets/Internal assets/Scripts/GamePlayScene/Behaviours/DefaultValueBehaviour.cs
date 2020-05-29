﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "DefaultValue", menuName = "CustomBehaviours/DefaultValueBehaviour")]
public class DefaultValueBehaviour : CustomBehaviour, ICustomBehaviour
{
    public void ReceiveEntityInstance(Entity entity)
    {
        InstanceEntity = entity;
    }

    public void ReceiveDataInstance<T>(T data)
    {
    }
}