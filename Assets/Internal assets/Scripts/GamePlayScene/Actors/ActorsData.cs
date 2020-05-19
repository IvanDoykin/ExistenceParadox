using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ActorsData", menuName = "DataStore/GamePlayData/ActorsData")]
public class ActorsData : ScriptableObject
{
    [SerializeField] private EnemiesData enemies;

    [SerializeField] private int health;

    public EnemiesData EnemiesData => enemies;

    public int Health
    {
        get => health;
        set => health = value;
    }
}