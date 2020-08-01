using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public IItemType itemType { get; private protected set; }

    [SerializeField]
    private Sprite itemIcon;
    public Sprite Icon { get => itemIcon; }

    [SerializeField]
    private string itemName;
    public string ItemName { get => itemName; }

    [SerializeField]
    private string itemDescription;
    public string ItemDescription { get => itemDescription; }

    [SerializeField]
    private string itemPrefab;
    public string  Prefab { get => itemPrefab; }

    private void OnTriggerEnter(Collider other)
    {
        InventoryControl.link.AddItem(this);
        Destroy(gameObject);
    }

    public interface IItemType
    {
        InventoryItem.ItemType AddItemType();
    }
}