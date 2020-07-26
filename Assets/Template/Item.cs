using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{

    [SerializeField]
    private Sprite icon;
    public Sprite Icon { get => icon; }

    [SerializeField]
    private string itemName;
    public string ItemName { get => itemName; }

    [SerializeField]
    private string itemDescription;
    public string ItemDescription { get => itemDescription; }

    [SerializeField]
    private string prefab;
    public string  Prefab { get => prefab; }

    private void OnTriggerEnter(Collider other)
    {
        InventoryControl.link.AddItem(this);
        Destroy(gameObject);
    }

}
