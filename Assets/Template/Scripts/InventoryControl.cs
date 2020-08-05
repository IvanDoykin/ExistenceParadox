using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryControl : MonoBehaviour
{
    public static InventoryControl link = null;
    [SerializeField]
    private List<GameObject> itemList = new List<GameObject>();
    [SerializeField]
    private GameObject prefabCell;
    

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

    public void EquipWeapon(Transform target, Transform cell)
    {
        if (cell.GetComponentInChildren<InventoryItem>() != null)
        {
            if (!cell.GetComponent<Cell>().IsEneble)
            {
                if(target.childCount != 0)
                {
                    Destroy(target.GetChild(0).gameObject);
                }

                GameObject.Instantiate(Resources.Load(cell.GetComponentInChildren<InventoryItem>().itemPrefab), target);
                cell.GetComponent<Cell>().IsEneble = true;
            }
            else
            {
                Destroy(target.GetChild(0).gameObject);
                cell.GetComponent<Cell>().IsEneble = false;
            }
        }
    }
}
