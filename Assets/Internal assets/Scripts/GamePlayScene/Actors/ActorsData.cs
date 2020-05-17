using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ActorsData", menuName = "DataStore/GamePlayData/ActorsData")]
public class ActorsData : ScriptableObject
{
    [SerializeField]
    private EnemiesData _enemies;

    [SerializeField]
    private int _health;

    public EnemiesData EnemiesData
    {
        get
        {
            return _enemies;
        }
    }

    public int Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
        }
    }
}
