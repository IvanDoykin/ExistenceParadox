using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    enum TypeCell
    {
        everyItem,
        WeaponMelee,
        WeaponDistance
    }
    [SerializeField]
    private TypeCell typeCell = TypeCell.everyItem;

    public bool CheckingItemType(string itemType)
    {
        if (typeCell.ToString() == itemType)
            return true;
        else return false;
    }

    private void OnTransformChildrenChanged()
    {
        if(typeCell == TypeCell.WeaponDistance || typeCell == TypeCell.WeaponMelee)
        {
            WeaponController.hng();
        }
    }
}
