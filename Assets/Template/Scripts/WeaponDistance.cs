using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDistance : Weapon, Item.IItemType
{
    public InventoryItem.ItemType AddItemType()
    {
        return InventoryItem.ItemType.WeaponDistanse;
    }

    private void Start()
    {
        itemType = this;
    }
}
