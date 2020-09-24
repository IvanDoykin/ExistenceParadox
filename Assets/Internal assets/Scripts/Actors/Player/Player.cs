using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Interactable
{
    public delegate void AccountHandler(int message);
    public static event AccountHandler Notify;

    public int Hp
    {
        get { return hp; }
        set
        {
            if (hp == value) return;
            hp = value;
            Notify?.Invoke(Hp);
        }
    }

    private void Awake()
    {
        Notify?.Invoke(Hp);
    }

    public override void CauseDamage(int damage)
    {
        if (Hp == 0) 
            return;

        Hp -= damage;
        if (Hp <= 0)
        {
            Hp = 0;
            Debug.Log("you dead");
        }
    }
}
