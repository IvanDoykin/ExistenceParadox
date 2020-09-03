using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    protected internal enum TypeCell
    {
        everyItem,
        WeaponMelee,
        WeaponDistance
    }
    [SerializeField]
    protected internal TypeCell typeCell = TypeCell.everyItem;

    public virtual bool CheckingItemType(string itemType) => true;

    //private void OnTransformChildrenChanged()
    //{
    //    if (typeCell == TypeCell.WeaponDistance || typeCell == TypeCell.WeaponMelee)
    //    {
    //        qickInventory.ff();
    //        //WeaponController.hng();
    //    }
    //}
}
