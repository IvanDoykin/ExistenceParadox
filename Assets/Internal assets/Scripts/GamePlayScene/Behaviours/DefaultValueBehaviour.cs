using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;


[CreateAssetMenu(fileName = "DefaultValue", menuName = "CustomBehaviours/DefaultValueBehaviour")]
public class DefaultValueBehaviour : CustomBehaviour
{
    protected override void ReceiveEntityInstanceData(Dictionary<Entity, Dictionary<string, Data>> dataDictionary, int entityNumber, string typeName, out Data currentData)
    {
        throw new System.NotImplementedException();
    }

    protected override void InitializeCurrentBehaviourByReceivedEntityInstance(Entity instance)
    {
        throw new System.NotImplementedException();
    }

    protected override void ReceiveAllData()
    {
        throw new System.NotImplementedException();
    }

    protected override void DeactivateCurrentInstanceModule<T>(Entity argument)
    {
    }
}