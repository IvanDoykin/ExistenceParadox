using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Item : MonoBehaviour
{
    //public IItemType ItemType { get; private protected set; }

    [SerializeField]
    private Sprite itemIcon = null;
    public Sprite Icon { get => itemIcon; }

    [SerializeField]
    private string itemName = null;
    public string ItemName { get => itemName; }

    [SerializeField]
    private string itemDescription = null;
    public string ItemDescription { get => itemDescription; }

    [SerializeField]
    private string itemPrefab = null;
    public string  Prefab { get => itemPrefab; }

    private void OnTriggerEnter(Collider other)
    {
        InventoryControl.link.AddItem(this);
    }

    public abstract object m_GetType();
}