using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class InfoWeaponCell
{
    [SerializeField]
    private Transform cell = null;
    public Transform Cell => cell;

    public InventoryItem cellInventoryItem => cell?.GetComponentInChildren<InventoryItem>();
    

}


public class WeaponController : MonoBehaviour
{

    public delegate void ItemActionHandler();
    public static event ItemActionHandler Notify_1;

    [SerializeField]
    private List<InfoWeaponCell> QuickItemList = new List<InfoWeaponCell>();

    [SerializeField]
    private Transform handT = null;

    private InfoWeaponCell activeCell;
    public InfoWeaponCell ActiveCell
    {
        get { return activeCell; }
        set
        {
            ClearChildrenFromHands();
            if (activeCell == value)
            {
                activeCell = null;
            }
            else
            {
                activeCell = value;
                InstantiateWeapon();
            }
        }
    }

    private Plane m_Plane;
    private Vector3 m_DistanceFromCamera;


    private void Start()
    {
        m_DistanceFromCamera = new Vector3(Camera.main.transform.position.x, 0.0f, Camera.main.transform.position.z);
        m_Plane = new Plane(Vector3.up, m_DistanceFromCamera);
    }

    private void InstantiateWeapon()
    {
        if (ActiveCell.cellInventoryItem)
        {
            ClearChildrenFromHands();
            GameObject.Instantiate(Resources.Load(ActiveCell.cellInventoryItem.itemPrefab), handT);
        }
    }

    public void InputWeapon(int cellIndex) => ActiveCell = QuickItemList[cellIndex];
    private void ClearChildrenFromHands()
    {
        int i = 0;

        GameObject[] allChildren = new GameObject[handT.childCount];

        if (allChildren.Length == 0)
            return;

        
        foreach (Transform child in handT)
        {
            allChildren[i] = child.gameObject;
            i += 1;
        }

        foreach (GameObject child in allChildren)
        {
            DestroyImmediate(child.gameObject);
        }
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
            Notify_1?.Invoke();
        }
    }
}
