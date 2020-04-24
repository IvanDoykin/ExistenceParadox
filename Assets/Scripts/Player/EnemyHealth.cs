using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private int healthPoint = 100;

	public void AddDamage(int damage)
	{
		healthPoint -= damage;
		if (healthPoint <= 0)
		{
			Destroy(gameObject);
		}
	}
}
