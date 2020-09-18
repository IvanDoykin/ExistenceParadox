using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] private protected int hp;
    [SerializeField] private float moveSpeed;
    public virtual void CauseDamage(int damage)
    {
        hp -= damage;
        if(hp <= 0)
        {
            Debug.Log("you dead");
            Destroy(gameObject);
        }
    }
}
