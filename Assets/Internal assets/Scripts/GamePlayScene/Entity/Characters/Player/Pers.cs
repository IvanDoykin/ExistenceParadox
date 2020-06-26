using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TopAdventure.Unity;

public class Pers : Entity
{
    [Foldout("DATA", true)]
    [SerializeField] private PlayerMovementData personData;

    private void Awake()
    {
        WriteCollectedData(personData);
        Initialize();
    }
}
