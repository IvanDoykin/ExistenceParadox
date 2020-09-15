using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickCell : Cell
{
    private enum TypeCell
    {
        WeaponMelee,
        WeaponDistance
    }

    [SerializeField]
    private TypeCell typeCell = TypeCell.WeaponDistance;
    private QuickInventoryPanel qickInventory;

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
        if (transform.childCount == 0)
        {
            childT = null;
            ItemData = null;
            qickInventory.CheckingRemovedCell(this);
        }
        else if(transform.childCount == 1)
        {
            childT = transform.GetChild(0);
            ItemData = gameObject?.GetComponentInChildren<InventoryItem>();
        }
    }
}
