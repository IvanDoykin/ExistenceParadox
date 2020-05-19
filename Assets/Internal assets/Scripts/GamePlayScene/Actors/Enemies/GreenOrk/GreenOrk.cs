using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class GreenOrk : MonoBehaviour
{
    [SerializeField] private GreenOrkDynamicData data;
    private readonly GreenOrkBehaviour _book = new GreenOrkBehaviour();

    [System.Serializable]
    private struct GreenOrkDynamicData
    {
        [SerializeField] private EnemyData enemyData;
        public int health;
        public float damage;


        public int BaseHealth
        {
            get => enemyData.Health;
            set => enemyData.Health = value;
        }

        public float BaseDamage
        {
            get => enemyData.Damage;
            set => enemyData.Damage = value;
        }
    }


    private class GreenOrkBehaviour : Enemy.EnemyBehaviour
    {
        public void Moving()
        {
            Fly();
        }
    }


    private void Start()
    {
        data.health = data.BaseHealth;
        data.damage = data.BaseDamage;
        _book.Moving();
    }
}