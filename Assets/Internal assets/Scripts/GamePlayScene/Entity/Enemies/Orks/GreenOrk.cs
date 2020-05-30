using System;
using System.Collections;
using System.Collections.Generic;
using TopAdventure.Unity;
using UnityEngine;

public class GreenOrk : Entity
{
    [Foldout("DATA", true)] [SerializeField]
    private MovementData movementData;

    public HpData hpData;

    [SerializeField] private DamageData damageData;

    [SerializeField] private PursueData pursueData;

    private void Start()
    {
        Initialize();
    }
}