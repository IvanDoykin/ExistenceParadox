using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GreenOrk : MonoBehaviour
{
    [System.Serializable]
    public struct GreenOrkDynamicData
    {
        [SerializeField] private GamePlaySceneData gamePlaySceneData;
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
            get { return _actorsHealth = gamePlaySceneData.ActorsData.Health; }
            set => _actorsHealth = value;
        }
    }


    [SerializeField] private GreenOrkDynamicData data;

    private void Start()
    {
        data.Damage = 10f;
        data.ActorsHealth = 250;
    }


    // Update is called once per frame
    void Update()
    {
    }
}