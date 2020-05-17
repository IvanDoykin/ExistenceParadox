using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GreenOrksData", menuName = "DataStore/GamePlayData/ActorsData/EnemiesData/GreenOrksData")]
public class GreenOrksData : ScriptableObject
{
    [SerializeField]
    private int _health;

    [SerializeField]
    private float _damage;

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

    public float Damage
    {
        get
        {
            return _damage;
        }
        set
        {
            _damage = value;
        }
    }
}
