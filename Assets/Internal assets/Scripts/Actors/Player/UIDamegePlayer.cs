using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDamegePlayer : MonoBehaviour
{
    private Player player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    public void Damage()
    {
        player.CauseDamage(20);
    }
}
