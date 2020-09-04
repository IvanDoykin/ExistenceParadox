using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField]
    private protected int hp;
    float moveSpeed;
    public virtual void CauseDamage(int damege)
    {
        hp -= damege;
        if(hp <= 0)
        {
            Debug.Log("you dead");
            Destroy(gameObject);
        }
    }
}
