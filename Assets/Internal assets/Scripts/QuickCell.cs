using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickCell : Cell
{
    private QickInventoryPanel qickInventory;
    private InventoryItem itemDate;

    public InventoryItem ItemData 
    { 
        get => itemDate; 
        set => itemDate = value; 
    }

    private void Awake()
    {
        qickInventory = GetComponentInParent<QickInventoryPanel>();
    }

    public override bool CheckingItemType(string itemType)
    {
        if (typeCell.ToString() == itemType)
            return true;
        else return false;
    }

    private void OnTransformChildrenChanged()
    {
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
