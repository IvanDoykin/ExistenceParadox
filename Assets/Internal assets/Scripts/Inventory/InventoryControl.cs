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
    private GameObject prefabCell = null;

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

    public void AddItem(Item item)
    {
        GameObject EmptyCell = FindEmptyCell();
        if (EmptyCell == null)
            return;

        GameObject newItem = Instantiate(prefabCell, EmptyCell.transform);
        InventoryItem inventoryItem = newItem.GetComponent<InventoryItem>();
        
        SetField(item.Icon, item.ItemName, item.ItemDescription, item.Prefab, item.GetType().ToString());

        void SetField(Sprite itemIcon, string itemName, string itemDescription, string prefab, string itemType)
        {
            newItem.GetComponent<Image>().sprite = itemIcon;
            inventoryItem.itemName = itemName;
            inventoryItem.itemDescription = itemDescription;
            inventoryItem.itemPrefab = prefab;
            inventoryItem.itemType = itemType;
        }

        Destroy(item.gameObject);
    }
}

