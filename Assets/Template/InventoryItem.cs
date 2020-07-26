using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public enum ItemType
    {
        WeaponMelee,
        WeaponDistanse
    }


    public ItemType itemType;
    public string itemName;
    public string itemDescription;
    public string prefab;

    private Vector3 startPosition;
    private Transform startParent;
    private Image image;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
        startParent = transform.parent;
        startPosition = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Transform targetCell = eventData.pointerCurrentRaycast.gameObject.transform;
        Cell cell = targetCell.GetComponent<Cell>();
        if (cell == null)
            return;
        //Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);
        if(cell.CheckingItemType(itemType))
        {
            transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform);
            transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position;
        }
        else
        {
            transform.position = startPosition;
        }
        image.raycastTarget = true;
    }
}
