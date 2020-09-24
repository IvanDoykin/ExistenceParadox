using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] protected int hp;
    private float moveSpeed;

    public abstract void CauseDamage(int damage);
}
