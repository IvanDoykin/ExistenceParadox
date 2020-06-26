using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursueDataCash
{
    public readonly Entity entity;
    public PursueData pursueData;

    public PursueDataCash(Entity entity)
    {
        this.entity = entity;
        ReceiveData();
    }

    private void ReceiveData()
    {
        if (entity.entityDataDictionary.TryGetValue("PursueData", out Data receivedData))
        {
            pursueData = (PursueData) receivedData;
        }
    }
}