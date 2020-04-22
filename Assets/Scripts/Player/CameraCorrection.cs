using UnityEngine;
using System.Collections;

public class CameraCorrection : MonoBehaviour
{

	public Transform target;
	public float distanceOffset = 0.25f;

	void Update()
	{
		//transform.position = ThridPersonCamera.positionCorrection.position;
		//transform.forward = ThridPersonCamera.positionCorrection.forward;
		//RaycastHit hit;
		//Vector3 trueTargetPosition = target.transform.position;
		//if (Physics.Linecast(trueTargetPosition, transform.position, out hit))
		//{
		//	float tempDistance = Vector3.Distance(trueTargetPosition, hit.point);
		//	Vector3 position = target.position - (transform.rotation * Vector3.forward * (tempDistance - distanceOffset));
		//	transform.position = new Vector3(position.x, ThridPersonCamera.targetHeight, position.z);
		//}
	}
}