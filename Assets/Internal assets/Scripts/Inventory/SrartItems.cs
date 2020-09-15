using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SrartItems : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> startItemList = new List<GameObject>();


    private void Start()
    {
        foreach (GameObject item in startItemList)
        {
            InventoryControl.link.AddItem(item.GetComponent<Item>());
        }
    }
}
