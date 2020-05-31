using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;


[CreateAssetMenu(fileName = "DefaultValue", menuName = "CustomBehaviours/DefaultValueBehaviour")]
public class DefaultValueBehaviour : CustomBehaviour, ICustomBehaviour
{
    public void InitializeBehaviourInstance(Entity entity)
    {
        InstanceEntity = entity;
    }

    protected override void ReceiveAllData()
    {
        throw new System.NotImplementedException();
    }

    
}