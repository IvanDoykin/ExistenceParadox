using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryCell : Cell
{
    public override bool CheckingItemType(string itemType) => true;

    private void OnTransformChildrenChanged()
    {
        if (transform.childCount == 0)
            childT = null;
        else if (transform.childCount == 1)
            childT = transform.GetChild(0);
    }
}
