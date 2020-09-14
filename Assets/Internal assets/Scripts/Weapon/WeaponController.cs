using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponController : MonoBehaviour
{

    public delegate void ItemActionHandler();
    public static event ItemActionHandler AttackEvent;

    [SerializeField] private Transform handT = null;
    [SerializeField] private Slider slider = null;
    public void Gun()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000.0f))
        {
            Vector3 hitPoint = hit.point;
            Vector3 oldRotate = Vector3.zero;
            handT.transform.LookAt(hitPoint);
            oldRotate.x = handT.transform.localEulerAngles.x;
            handT.transform.localEulerAngles = oldRotate;
            AttackEvent?.Invoke();
        }
    }
    
    public void InputReload()
    {
        WeaponDistance weaponDistance = handT.GetComponentInChildren<WeaponDistance>();
        if(weaponDistance != null)
        {
            weaponDistance.ReloadWeapon(slider);
        }
    }
}
