using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private float turnsSmoothTime = 0.2f;
    private float turnSmoothVelocity;
    public GameObject prefab;
    public Transform prefab_2;
    private void Update()
    {
        Vector3 oldPos = transform.rotation.eulerAngles;
        Debug.Log(oldPos);
        transform.LookAt(prefab_2.transform);
        oldPos.x = transform.eulerAngles.x;
        transform.eulerAngles = oldPos;

        //Vector3 targetRotation = new Vector3(Camera.main.transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
        //transform.eulerAngles = targetRotation;
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(prefab, transform.position, transform.rotation);
        }
    }

}
