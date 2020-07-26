using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    enum TypeCell
    {
        every,
        melee,
        distanse
    }
    [SerializeField]
    private TypeCell typeCell;


    public bool CheckingItemType(InventoryItem.ItemType itemType)
    {
        switch(typeCell)
        {
            case TypeCell.melee:
                if (itemType == InventoryItem.ItemType.WeaponMelee)
                    return true;
                else  return false;

            case TypeCell.distanse:
                if (itemType == InventoryItem.ItemType.WeaponDistanse)
                    return true;
                else return false;
            default:
                return true;
        }
    }
}
