using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class InfoWeaponCell
{
    [SerializeField]
    private Transform cell = null;

    private bool isEneble = false;

    public bool IsEneble { get => isEneble; set => isEneble = value; }

    public Transform Cell { get => cell; }
}


public class WeaponController : MonoBehaviour
{
    [SerializeField]
    private List<InfoWeaponCell> QuickItemList = new List<InfoWeaponCell>();

    [SerializeField]
    private Transform handT = null;
    private InventoryItem Active;

    private Plane m_Plane;
    private Vector3 m_DistanceFromCamera;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        m_DistanceFromCamera = new Vector3(Camera.main.transform.position.x, 0.0f, Camera.main.transform.position.z);
        m_Plane = new Plane(Vector3.up, m_DistanceFromCamera);
    }

    public void InputWeapon(int cellIndex)
    {
        InfoWeaponCell infoQuickCell = QuickItemList[cellIndex];

        InventoryItem inventoryItem = infoQuickCell.Cell.GetComponentInChildren<InventoryItem>();

        if (inventoryItem)
        {

            if(!infoQuickCell.IsEneble)
            {
                ClearChildrenFromHands();

                GameObject.Instantiate(Resources.Load(inventoryItem.itemPrefab), handT);
                Active = inventoryItem;
                infoQuickCell.IsEneble = true;
            }
            else
            {
                ClearChildrenFromHands();
            }
        }
    }
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

        foreach (InfoWeaponCell item in QuickItemList)
        {
            item.IsEneble = false;
        }
    }
    public void Gun()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        float enter = 0.0f;

        if (m_Plane.Raycast(ray, out enter))
        {
            Vector3 hitPoint = ray.GetPoint(enter);
            Vector3 oldRotate = Vector3.zero;
            handT.transform.LookAt(hitPoint);
            oldRotate.x = handT.transform.localEulerAngles.x;
            handT.transform.localEulerAngles = oldRotate;
            //Instantiate(prefab, transform.position, transform.rotation);
            var item = Active.item.m_GetType();
            item.GetType();
            handT.GetComponent<>().Attack();
        }


    }
}
