using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public int damage = 50;
	public string[] targetTags = { "Enemy", "Target_2" };
	private float lifeTime = 10f;

	private void Update()
	{
		lifeTime -= Time.deltaTime;
		if (lifeTime <= 0)
		{
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter(Collider coll)
	{
		foreach (string currentTag in targetTags)
		{
			if (currentTag == coll.transform.tag)
			{
				coll.transform.GetComponent<EnemyHealth>().AddDamage(damage);
			}
		}
		Destroy(gameObject);
	}
}
