using System.Collections;
using System.Collections.Generic;
using TopAdventure.Unity;
using UnityEngine;

public class GreenOrk : Entity
{
    [Foldout("DATA", true)] 
    [SerializeField]
    private MovementData movementData;

    [SerializeField] private HpData hpData;

    [SerializeField] private DamageData damageData;
}