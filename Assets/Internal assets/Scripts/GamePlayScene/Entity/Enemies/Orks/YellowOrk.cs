using System.Collections;
using System.Collections.Generic;
using TopAdventure.Unity;
using UnityEngine;

public class YellowOrk : Entity
{
    [Foldout("DATA", true)] [SerializeField]
    private MovementData movementData;

    [SerializeField] private HpData hpData;
}