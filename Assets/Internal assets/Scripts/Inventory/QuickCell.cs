using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickCell : Cell
{
    private QuickInventoryPanel qickInventory;
    private InventoryItem itemDate;

    public InventoryItem ItemData 
    { 
        get => itemDate; 
        set => itemDate = value; 
    }

    private void Awake()
    {
        qickInventory = GetComponentInParent<QuickInventoryPanel>();
    }

    public override bool CheckingItemType(string itemType)
    {
        if (typeCell.ToString() == itemType)
            return true;
        else return false;
    }

    private void OnTransformChildrenChanged()
    {
        Debug.Log("ff");
        if (transform.childCount >= 1)
        {
            ItemData = gameObject?.GetComponentInChildren<InventoryItem>();
        }
        else if(transform.childCount == 0)
        {
            ItemData = null;
            qickInventory.CheckingRemovedCell(this);
        }
    }
}
