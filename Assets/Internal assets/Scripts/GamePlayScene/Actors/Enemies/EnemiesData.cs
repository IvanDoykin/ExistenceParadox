using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemiesData", menuName = "DataStore/GamePlayData/ActorsData/EnemiesData")]
public class EnemiesData : ScriptableObject
{
    [SerializeField]
    private GreenOrksData _greenOrks;

    public GreenOrksData GreenOrksData
    {
        get
        {
            return _greenOrks;
        }
    }
}
