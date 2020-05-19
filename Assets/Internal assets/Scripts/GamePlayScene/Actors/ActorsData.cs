using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ActorsData", menuName = "DataStore/GamePlayData/ActorsData")]
public class ActorsData : ScriptableObject
{
    [SerializeField] private int health;
    [SerializeField] private float damage;

    public int Health
    {
        get => health;
        set => health = value;
    }

    public float Damage
    {
        get => damage;
        set => damage = value;
    }
}