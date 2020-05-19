using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemiesData", menuName = "DataStore/GamePlayData/ActorsData/EnemiesData")]
public class EnemiesData : ScriptableObject
{
    [SerializeField] private GreenOrkData greenOrk;

    public GreenOrkData GreenOrkData => greenOrk;
}