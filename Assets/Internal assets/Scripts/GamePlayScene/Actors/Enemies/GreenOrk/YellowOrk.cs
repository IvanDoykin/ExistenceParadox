using System.Collections;
using System.Collections.Generic;
using TopAdventure.Unity;
using UnityEngine;

public class YellowOrk : Actor
{
    [Foldout("DATA", true)] [SerializeField]
    private DataMovement dataMovement;

    [SerializeField] private DataHp dataHp;
}