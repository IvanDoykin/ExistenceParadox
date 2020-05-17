using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TopAdventure;
using System;


[CreateAssetMenu(fileName = "GamePlaySceneData", menuName = "DataStore/GamePlaySceneData")]
public class GamePlaySceneData : ScriptableObject
{
    [SerializeField]
    private ActorsData _actors;

    

    public ActorsData ActorsData
    {
        get
        {
            return _actors;
        }
    }

    


}
