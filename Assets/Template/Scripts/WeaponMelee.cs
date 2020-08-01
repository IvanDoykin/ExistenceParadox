using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMelee : Weapon, Item.IItemType
{
    public InventoryItem.ItemType AddItemType()
    {
        return InventoryItem.ItemType.WeaponMelee;
    }

    private void Start()
    {
        itemType = this;
    }
}
