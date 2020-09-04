using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{

    public delegate void ItemActionHandler();
    public static event ItemActionHandler AttackEvent;

    [SerializeField]
    private Transform handT = null;

    private Plane m_Plane;
    private Vector3 m_DistanceFromCamera;


    private void Start()
    {
        m_DistanceFromCamera = new Vector3(Camera.main.transform.position.x, 0.0f, Camera.main.transform.position.z);
        m_Plane = new Plane(Vector3.up, m_DistanceFromCamera);
    }

    public void Gun()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


        if (m_Plane.Raycast(ray, out float enter))
        {
            Vector3 hitPoint = ray.GetPoint(enter);
            Vector3 oldRotate = Vector3.zero;
            handT.transform.LookAt(hitPoint);
            oldRotate.x = handT.transform.localEulerAngles.x;
            handT.transform.localEulerAngles = oldRotate;
            AttackEvent?.Invoke();
        }
    }
}
