using System.Collections;
using System.Collections.Generic;
using Packages.Rider.Editor;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "Pursue", menuName = "CustomBehaviours/Pursue")]
public class PursueBehaviour : CustomBehaviour, ICustomBehaviour, ITick
{
    public void ReceiveEntityInstance(Entity entity)
    {
        InstanceEntity = entity;

        ManagerUpdate.AddTo(this);
    }


    private void Pursue()
    {
        PursueData pursueData = ReceivePursueData();
        pursueData.navMeshAgent.destination = pursueData.player.transform.position;
    }

    private PursueData ReceivePursueData()
    {
        if (InstanceEntity.entityDataDictionary.TryGetValue("PursueData", out dynamic receivedHpData))
        {
            return ((PursueData) receivedHpData);
        }

        Debug.Log($"PursueData is not found in current entity: {InstanceEntity.GetType()}");
        return null;
    }


    public void Tick()
    {
        Pursue();
    }
}