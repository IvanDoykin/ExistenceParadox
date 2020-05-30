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
        // _navmesh.destination = player.transform.position;
        Pursue();
    }


    private void Pursue()
    {
        PursueData pursueData = ReceivePursueData();
        Debug.Log(pursueData);
    }

    private PursueData ReceivePursueData()
    {
        if (InstanceEntity.entityDataDictionary.TryGetValue("PursueData", out dynamic receivedHpData))
        {
            return ((PursueData) receivedHpData);
            // pursueData.navMesh.destination = InstanceEntity.transform.position;
        }

        Debug.Log($"PursueData is not found in current entity: {InstanceEntity.GetType()}");
        return null;
    }


    public void Tick()
    {
    }
}