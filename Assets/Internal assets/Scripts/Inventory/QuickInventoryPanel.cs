using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.OleDb;
using UnityEngine;
using UnityEngine.UI;

public class QuickInventoryPanel : MonoBehaviour
{
    public delegate void ItemTakeHandler(InventoryItem item);
    public static event ItemTakeHandler ItemTakeEvent;

    public delegate void ItemRemoveHandler();
    public static event ItemRemoveHandler ItemRemoveEvent;

    private List<QuickCell> QuickCellList = new List<QuickCell>();
    private int indexActiveCell = 0;

    public void StartTakeEvent(InventoryItem item) => ItemTakeEvent?.Invoke(item);

    public void CheckingRemovedCell(QuickCell quickCell)
    {
        if (quickCell == QuickCellList[indexActiveCell])
        {
            ItemRemoveEvent?.Invoke();
        }
    }

    private void Awake()
    {
        PlayerController.ChangingItemEvent += InputChandingItem;
        foreach (Transform child in transform)
        {
            QuickCellList.Add(child.gameObject.GetComponent<QuickCell>());
        }
        SetIndex(indexActiveCell);
    }

    private void SetIndex(int index)
    {
        int oldIndex = indexActiveCell;
        indexActiveCell = index;

        if (indexActiveCell >= QuickCellList.Count)
            indexActiveCell = 0;
        else if (indexActiveCell < 0)
            indexActiveCell = QuickCellList.Count - 1;

        if (QuickCellList[indexActiveCell] == QuickCellList[oldIndex])
            QuickCellList[indexActiveCell].SetActive(!QuickCellList[indexActiveCell].IsActive);
        else
        {
            QuickCellList[oldIndex].SetActive(false);
            QuickCellList[indexActiveCell].SetActive(true);
        }
    }

    private (int, Action<int>) InputChandingItem() => (indexActiveCell, SetIndex);
}
