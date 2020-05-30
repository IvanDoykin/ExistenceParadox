using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;


[CreateAssetMenu(fileName = "DefaultValue", menuName = "CustomBehaviours/DefaultValueBehaviour")]
public class DefaultValueBehaviour : CustomBehaviour, ICustomBehaviour
{
    public void ReceiveEntityInstance(dynamic entity)
    {
        InstanceEntity = entity;
    }
}