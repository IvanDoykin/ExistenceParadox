using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickInventoryPanel : MonoBehaviour
{
    [SerializeField]
    private Transform handT = null;
    private int indexActiveCell = -1;

    [SerializeField]
    private List<QuickCell> QuickCellList = new List<QuickCell>();

    public int IndexActiveCell 
    { 
        get => indexActiveCell;
        set
        {
            if (indexActiveCell == value)
            {
                indexActiveCell = -1;
                RemoveWeapon();
            }
            else
            {
                indexActiveCell = value;
                TakeWeapon(value);
            }     
        } 
    }

    private void Awake()
    {
        foreach (Transform child in transform)
        {
            QuickCellList.Add(child.gameObject.GetComponent<QuickCell>());
        }
    }

    private void TakeWeapon(int index)
    {
        ClearChildrenFromHands();
        QuickCell quickCell = QuickCellList[index];
        if(quickCell.ItemData)
        {
            quickCell.gameObject.GetComponent<Image>().color = Color.red;
            Debug.Log("Экипирован");
        }
    }

    private void RemoveWeapon()
    {
        ClearChildrenFromHands();
    }

    private void ClearChildrenFromHands()
    {
        foreach (QuickCell cell in QuickCellList)
        {
            cell.gameObject.GetComponent<Image>().color = Color.white;
        }

        int i = 0;

        GameObject[] allChildren = new GameObject[handT.childCount];

        if (allChildren.Length == 0)
            return;

        foreach (Transform child in handT)
        {
            allChildren[i] = child.gameObject;
            i += 1;
        }

        foreach (GameObject child in allChildren)
        {
            DestroyImmediate(child.gameObject);
        }
    }

    public void CheckingRemovedCell(QuickCell quickCell)
    {
        if(quickCell == QuickCellList[indexActiveCell])
        {
            ClearChildrenFromHands();
        }
    }
}
