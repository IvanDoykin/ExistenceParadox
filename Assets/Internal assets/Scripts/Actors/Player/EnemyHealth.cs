using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    [SerializeField] private int healthPoint = 100;

	public void TakeDamage(int damage)
	{
		healthPoint -= damage;
		if (healthPoint <= 0)
		{
			Destroy(gameObject);
		}
	}
}
