using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour//, IBeginDragHandler, IDragHandler, IEndDragHandler
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

    //public void OnBeginDrag(PointerEventData eventData)
    //{
    //    itemIcon.raycastTarget = false;
    //    startParent = transform.parent;
    //    startPosition = transform.position;
    //}

    //public void OnDrag(PointerEventData eventData)
    //{

    //    transform.position = eventData.position;
    //}

    //public void OnEndDrag(PointerEventData eventData)
    //{
    //    Transform targetCell = eventData.pointerCurrentRaycast.gameObject.transform;
    //    Cell cell = targetCell.GetComponent<Cell>();
    //    if (cell == null)
    //        return;
    //    //Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);
    //    if(cell.CheckingItemType(itemType))
    //    {
    //        transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform);
    //        transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position;
    //    }
    //    else
    //    {
    //        transform.position = startPosition;
    //    }
    //    itemIcon.raycastTarget = true;
    //}
    
}
