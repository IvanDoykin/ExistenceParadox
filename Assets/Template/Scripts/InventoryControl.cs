using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class InfoQuickCell
{
    [SerializeField]
    private Transform cell = null;

    private bool isEneble = false;

    public bool IsEneble { get => isEneble; set => isEneble = value; }

    public Transform Cell { get => cell; }
}

public class InventoryControl : MonoBehaviour
{
    public static InventoryControl link = null;

    [SerializeField]
    private List<GameObject> itemList = new List<GameObject>();

    [SerializeField]
    private List<InfoQuickCell> QuickItemList = new List<InfoQuickCell>();

    [SerializeField]
    private GameObject prefabCell = null;

    [SerializeField]
    private Transform handT = null;


    private void Awake()
    {
        link = this;
    }

    private GameObject FindEmptyCell()
    {
        foreach (GameObject cell in itemList)
        {
            if (cell.GetComponentInChildren<InventoryItem>() == null)
            {
                return cell;
            }
        }
        return null;
    }

    public void AddItem(object item)
    {
        GameObject EmptyCell = FindEmptyCell();
        if (EmptyCell == null)
            return;

        GameObject newItem = Instantiate(prefabCell, EmptyCell.transform);
        InventoryItem inventoryItem = newItem.GetComponent<InventoryItem>();
        Item new_Item = (Item)item;
        SetField(new_Item.Icon, new_Item.ItemName, new_Item.ItemDescription, new_Item.Prefab, new_Item.itemType.AddItemType());

        void SetField(Sprite Icon, string itemName, string itemDescription, string prefab, InventoryItem.ItemType itemType)
        {
            newItem.GetComponent<Image>().sprite = Icon;
            inventoryItem.itemName = itemName;
            inventoryItem.itemDescription = itemDescription;
            inventoryItem.itemPrefab = prefab;
            inventoryItem.itemType = itemType;
        }
        Destroy(new_Item.gameObject);
    }

    public void EquipWeapon(Transform target, int cellIndex)
    {
        if (QuickItemList[cellIndex].Cell.GetComponentInChildren<InventoryItem>() != null)
        {
            if (!QuickItemList[cellIndex].IsEneble)
            {
                if (target.childCount != 0)
                {
                    ClearChildrenFromHands();

                    foreach (InfoQuickCell item in QuickItemList)
                    {
                        item.IsEneble = false;
                    }
                }

                GameObject.Instantiate(Resources.Load(QuickItemList[cellIndex].Cell.GetComponentInChildren<InventoryItem>().itemPrefab), target);

                QuickItemList[cellIndex].IsEneble = true;
            }
            else
            {
                ClearChildrenFromHands();

                QuickItemList[cellIndex].IsEneble = false;
            }
        }

        void ClearChildrenFromHands()
        {
            int i = 0;

            GameObject[] allChildren = new GameObject[handT.childCount];

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
    }

}

