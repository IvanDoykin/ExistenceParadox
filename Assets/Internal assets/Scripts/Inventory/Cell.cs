using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cell : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    protected internal enum TypeCell
    {
        everyItem,
        WeaponMelee,
        WeaponDistance
    }
    [SerializeField]
    protected internal TypeCell typeCell = TypeCell.everyItem;

    private Vector3 startPosition;
    private Transform childT = null;

    public virtual bool CheckingItemType(string itemType) => true;


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

        if (cell == null)
        {
            childT.position = startPosition;
            return;
        }
        else
        {
            if (cell.CheckingItemType(childT.GetComponent<InventoryItem>().itemType.ToString()))
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

    private void OnTransformChildrenChanged()
    {
        if (transform.childCount == 0)
            childT = null;
        else if (transform.childCount == 1)
            childT = transform.GetChild(0);
    }
}
