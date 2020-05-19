using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class GreenOrk : MonoBehaviour
{
    [System.Serializable]
    public struct GreenOrkDynamicData
    {
        [SerializeField] private ActorsData actorsData;
        [SerializeField] private GreenOrkData greenOrkData;

        [SerializeField] private int health;

        [SerializeField] private float damage;
        private int _actorsHealth;


        internal int Health
        {
            get { return health = greenOrkData.Health; }
            set => health = value;
        }

        internal float Damage
        {
            get { return damage = greenOrkData.Damage; }
            set => damage = value;
        }

        internal int ActorsHealth
        {
            get { return _actorsHealth = actorsData.Health; }
            set => _actorsHealth = value;
        }
    }
    
    private readonly GreenOrkBehaviour _book = new GreenOrkBehaviour();

    private class GreenOrkBehaviour : Enemy
    {
     
    }

    [SerializeField] private GreenOrkDynamicData data;


    private void Start()
    {
        data.Damage = 10f;
        data.ActorsHealth = 250;
        
        _book.Fly();
        
       
    }
}