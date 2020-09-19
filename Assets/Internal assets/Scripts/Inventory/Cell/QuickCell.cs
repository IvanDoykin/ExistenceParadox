using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickCell : Cell
{

    private enum TypeCell
    {
        WeaponMelee,
        WeaponDistance
    }

    [SerializeField] private TypeCell typeCell = TypeCell.WeaponDistance;
    private bool isActive = false;
    private Image image = null;
    private QuickInventoryPanel qickInventory = null;

    public bool IsActive 
    { 
        get => isActive; 
        private set
        {
            isActive = value;
            if(isActive)
                qickInventory.StartTakeEvent(GetItem());
        }
    }

    private void Awake()
    {
        image = GetComponent<Image>();
        qickInventory = GetComponentInParent<QuickInventoryPanel>();
    }

    public InventoryItem GetItem() => GetComponentInChildren<InventoryItem>();

    public void SetActive(bool actice)
    {
        IsActive = actice;
        if (IsActive)
            image.color = Color.red;
        else
            image.color = Color.white;
        
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
        else if (transform.childCount == 1)
        {
            childT = transform.GetChild(0);
            ItemData = gameObject?.GetComponentInChildren<InventoryItem>();
        }
    }
}
