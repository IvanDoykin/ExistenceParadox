using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponController : MonoBehaviour
{

    public delegate void ItemActionHandler();
    public static event ItemActionHandler AttackEvent;
 
    [SerializeField] private Transform handPoint = null;
    [SerializeField] private Slider slider = null;

    private void TakeWeapon(InventoryItem item)
    {

        Debug.Log("Take");
        //    ClearChildrenFromHands();
        //    QuickCell quickCell = QuickCellList[index];
        //    if (quickCell.ItemData)
        //    {
        //        quickCell.gameObject.GetComponent<Image>().color = Color.red;
        //        GameObject item = GameObject.Instantiate(Resources.Load(quickCell.ItemData.itemPrefab), handPoint) as GameObject;
        //        item.GetComponent<BoxCollider>().enabled = false;
        //    }
    }

    public void Gun()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000.0f))
        {
            Vector3 hitPoint = hit.point;
            Vector3 oldRotate = Vector3.zero;
            handPoint.transform.LookAt(hitPoint);
            oldRotate.x = handPoint.transform.localEulerAngles.x;
            handPoint.transform.localEulerAngles = oldRotate;
            AttackEvent?.Invoke();
        }
    }

    public void InputReload()
    {
        WeaponDistance weaponDistance = handPoint.GetComponentInChildren<WeaponDistance>();
        if(weaponDistance != null)
        {
            weaponDistance.ReloadWeapon(slider);
        }
    }

    private void Awake()
    {
        QuickInventoryPanel.ItemTakeEvent += TakeWeapon;
        QuickInventoryPanel.ItemRemoveEvent += RemoveWeapon;
    }

    private void RemoveWeapon()
    {

        ClearChildrenFromHands();
    }

    private void ClearChildrenFromHands()
    {
        int i = 0;

        GameObject[] allChildren = new GameObject[handPoint.childCount];

        if (allChildren.Length == 0)
            return;

        foreach (Transform child in handPoint)
        {
            allChildren[i] = child.gameObject;
            i += 1;
        }

        foreach (GameObject child in allChildren)
        {
            Destroy(child.gameObject);
        }
    }
}
