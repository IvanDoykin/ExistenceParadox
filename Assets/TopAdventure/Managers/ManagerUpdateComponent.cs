using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// Это менеджер(класс), который наследуется от MonoBehaviour? и именно здесь вызваются встроенные в unity методы Update
/// </summary>
public class ManagerUpdateComponent : MonoBehaviour
{

	private ManagerUpdate mng;

	public void Setup(ManagerUpdate mng)
	{
		this.mng = mng;
	}


	private void Update()
	{
		mng.Tick();
	}

	private void FixedUpdate()
	{
		mng.TickFixed();
	}


	private void LateUpdate()
	{
		mng.TickLate();
	}
	
	
}
