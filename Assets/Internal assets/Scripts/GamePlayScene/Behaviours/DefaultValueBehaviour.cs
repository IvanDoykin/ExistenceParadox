﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;


[CreateAssetMenu(fileName = "DefaultValue", menuName = "CustomBehaviours/DefaultValueBehaviour")]
public class DefaultValueBehaviour : CustomBehaviour
{
    protected override void ReceiveAllData()
    {
        throw new System.NotImplementedException();
    }

    protected override void DeactivateCurrentInstanceModule<T>(T argument)
    {
    }
}