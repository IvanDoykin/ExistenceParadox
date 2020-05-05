﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
	public Rigidbody bullet;
	public Transform gunPoint;
	public int bulletSpeed = 10;
	public int damage;
	public float timeout = 0.5f;
	private float curTimeout;

	void Update()
	{
		RaycastHit hit;
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