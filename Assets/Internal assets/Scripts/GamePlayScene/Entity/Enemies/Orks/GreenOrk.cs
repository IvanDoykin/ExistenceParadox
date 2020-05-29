using System;
using System.Collections;
using System.Collections.Generic;
using TopAdventure.Unity;
using UnityEngine;

public class GreenOrk : Entity, IEntityDataContainer
{
    private string _instanceId;

    [Foldout("DATA", true)] [SerializeField]
    private MovementData movementData;

    [SerializeField] private HpData hpData;

    [SerializeField] private DamageData damageData;

    [SerializeField] private PursueData pursueData;

    private void Awake()
    {
        _instanceId = GetInstanceID().ToString();
        WriteCollectedData(hpData, damageData, pursueData);
    }


    public void WriteCollectedData(params Data[] dataVariables)
    {
        foreach (var data in dataVariables)
        {
            entityDataDictionary.Add(_instanceId, data);
        }
    }
}