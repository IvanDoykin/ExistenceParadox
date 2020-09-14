using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Cell : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    protected Vector3 startPosition;
    protected Transform childT = null;
    public InventoryItem ItemData { get; protected set; }


    public abstract bool CheckingItemType(string itemType);

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = childT.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        childT.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Transform targetCell = eventData.pointerCurrentRaycast.gameObject?.transform;
        Cell cell = targetCell?.GetComponent<Cell>();
        if (!eventData.pointerCurrentRaycast.gameObject)
        {
            Debug.Log("ffff");
        }
        else if (cell == null)
        {
            childT.position = startPosition;
            return;
        }
        else
        {
            if (cell.CheckingItemType(childT.GetComponent<InventoryItem>().itemType))
            {
                childT.position = targetCell.position;
                childT.SetParent(targetCell);
            }
            else
            {
                childT.position = startPosition;
            }
        }
    }
}
