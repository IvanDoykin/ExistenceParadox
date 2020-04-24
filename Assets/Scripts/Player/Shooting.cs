using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
	public Rigidbody bullet;
	public Transform gunPoint;
	public int bulletSpeed = 10;
	//public float timeout = 0.5f;
	//private float curTimeout;

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			//curTimeout += Time.deltaTime;
			//if (curTimeout > timeout)
			//{
			//curTimeout = 0;
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(transform.position);
			Physics.Raycast(ray, out hit);
			Vector3 rotationTarget = new Vector3(hit.point.x,0,hit.point.z);
			transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(rotationTarget), 10 * Time.deltaTime);
			Rigidbody bulletInstance = Instantiate(bullet, gunPoint.position, Quaternion.identity) as Rigidbody;
			bulletInstance.velocity = gunPoint.forward * bulletSpeed;
			//}
		}
		//else
		//{
		//	curTimeout = timeout + 1;
		//}
	}

}
