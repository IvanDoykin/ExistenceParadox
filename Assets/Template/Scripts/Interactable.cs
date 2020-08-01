using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public  int hp;
    float moveSpeed;
    public void CauseDamage(int damege)
    {
        hp -= damege;
        if(hp <= 0)
        {
            Debug.Log("you dead");
        }
    }
}
