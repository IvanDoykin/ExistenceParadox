using System;
using System.Collections;
using System.Collections.Generic;
using TopAdventure.Unity;
using UnityEngine;

public class GreenOrk : Entity
{
    [Foldout("DATA", true)] [SerializeField]
    private MovementData movementData;


    [SerializeField] private DamageData damageData;

    [SerializeField] private PursueData pursueData;

    private void Awake()
    {
        WriteCollectedData(damageData, pursueData);
        Initialize();
    }

    private void WriteCollectedData(params Data[] dataVariables)
    {
        foreach (var currentData in dataVariables)
        {
            entityDataDictionary.Add(currentData.GetType().ToString(), currentData);
        }
    }
}