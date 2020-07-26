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
    private GameObject cell;

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

        GameObject newItem = Instantiate(cell, EmptyCell.transform);
        InventoryItem inventoryItem = newItem.GetComponent<InventoryItem>();

        switch (item)
        {
            case WeaponMelee w:
                inventoryItem.itemType = InventoryItem.ItemType.WeaponMelee;
                SetField(w.Icon, w.ItemName, w.ItemDescription, w.Prefab);
                break;
            case WeaponDistance w:
                inventoryItem.itemType = InventoryItem.ItemType.WeaponDistanse;
                SetField(w.Icon, w.ItemName, w.ItemDescription, w.Prefab);
                break;

            default:
                break;
        }

        void SetField(Sprite Icon, string itemName, string itemDescription, string prefab)
        {
            newItem.GetComponent<Image>().sprite = Icon;
            inventoryItem.itemName = itemName;
            inventoryItem.itemDescription = itemDescription;
            inventoryItem.prefab = prefab;
        }

    }
}
