using System.Collections; //useless usings
using System.Collections.Generic; //useless usings
using UnityEngine;

public class Shooting : MonoBehaviour, ITick
{
    //write in VK with that
	public Rigidbody bullet;
	public Transform gunPoint;
	public int bulletSpeed = 10;
	public int damage;
	public float timeout = 0.5f;
	private float curTimeout;
    //lines 8-13

    private void Start()
    {
        ManagerUpdate.AddTo(this);
    }

    public void Tick()
	{
		RaycastHit hit; //that not use in code
		var Ray = Camera.main.ScreenPointToRay(Input.mousePosition);		
		if (Input.GetMouseButtonDown(0))
		{
			curTimeout += Time.deltaTime;
			if (curTimeout > timeout)
			{
				curTimeout = 0;

				var bulletInstance = Instantiate(bullet, gunPoint.position, Quaternion.identity);
				bulletInstance.GetComponent<Bullet>().SetDamage(damage);
				bulletInstance.AddForce(Ray.direction * bulletSpeed, ForceMode.Impulse);			
			}
		}
		else
		{
			curTimeout = timeout + 1;
		}
		
		Debug.DrawRay(Camera.main.transform.position, Ray.direction, Color.red);
	}

}
