using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GreenOrk : MonoBehaviour
{

    [System.Serializable]
    public struct GreenOrkDynamicData
    {

        [SerializeField]
        private GamePlaySceneData _gamePlaySceneData;
        [SerializeField]
        private GreenOrksData _data;

        [SerializeField]
        private int _health;

        [SerializeField]
        private float _damage;
        private int _actorsHealth;


        internal int Health
        {
            get
            {
                return _health = _data.Health;
            }
            set
            {
                _health = value;
            }
        }

        internal float Damage
        {
            get
            {
                return _damage = _data.Damage;
            }
            set
            {
                _damage = value;
            }
        }

        internal int ActorsHealth
        {
            get
            {
                return _actorsHealth = _gamePlaySceneData.ActorsData.Health;
            }
            set
            {
                _actorsHealth = value;
            }
        }
    }


    [SerializeField]
    private GreenOrkDynamicData _data;

    void Start()
    {
        _data.Damage = 10f;
        _data.ActorsHealth = 250;
    }



    // Update is called once per frame
    void Update()
    {

    }

}
