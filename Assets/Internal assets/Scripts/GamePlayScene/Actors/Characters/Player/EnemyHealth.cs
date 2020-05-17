using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour //this is data - very good
{

    [SerializeField] private int healthPoint = 100;

	public void TakeDamage(int damage) //but what take damage is doing here? We take damage too - will be repeating code
	{
		healthPoint -= damage;
		if (healthPoint <= 0)
		{
			Destroy(gameObject);
		}
	}
}
// this class should have ONLY enemy health. public int value = 100. VALUE, because class EnemyHealth 'say' that this have Health, okay?