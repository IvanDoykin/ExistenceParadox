using UnityEngine;

public class Bullet : MonoBehaviour, ITick
{
	public int damage = 50;                     //write in VK 
	private float lifeTime = 10f;               //for discussion
    public string[] targetTags = { "Enemy" };   //What is hardcode? If u create 100 types of enemies, u should set tags 100 times.

    public void Tick()  //Change Update on public void Tick() from interface ITick
	{
		lifeTime -= Time.deltaTime;
		if (lifeTime <= 0)
		{
			Destroy(gameObject);
		}
	}

	public void SetDamage(int dmg)
	{
		damage = dmg;
	}

    void OnTriggerEnter(Collider coll) // add modif. 'private'. Will no change, but one style for ever script.
	{
		foreach (string currentTag in targetTags)
		{
			if (currentTag == coll.transform.tag)
			{
				coll.transform.GetComponent<EnemyHealth>().TakeDamage(damage); //read comments at lines 5-6
			}
		}
		Collider[] colliders = Physics.OverlapSphere(transform.position, 5f); //why 5f? why not 500f? add const float radius = 5f
		foreach (Collider hit in colliders)
		{
			Rigidbody rb = hit.GetComponent<Rigidbody>();

			if (rb != null)
				rb.AddExplosionForce(50000f, transform.position, 15f); //again 'magic numbers' what is 50000f? what is 15f?
		}
		Destroy(gameObject);
	}
}
