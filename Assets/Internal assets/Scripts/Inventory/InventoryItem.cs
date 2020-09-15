using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{

    public string itemType;
    public string itemName;
    public string itemDescription;
    public string itemPrefab;

    private Vector3 startPosition;
    private Transform startParent;
    private Image itemIcon;

    private void Start()
    {
        itemIcon = GetComponent<Image>();
    }
}
