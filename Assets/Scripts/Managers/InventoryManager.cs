using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    [SerializeField] private List<GameObject> itemSlots;

    private void Start()
    {
        AddToList();
    }

    private void AddToList()
    {
        ItemSlot[] slots = GetComponentsInChildren<ItemSlot>();
        foreach (var item in slots)
        {
            itemSlots.Add(item.gameObject);
        }
    }

    public bool AddItem(GameObject item)
    {

        for (int i = 0; i < itemSlots.Count; i++)
        {
            if(itemSlots[i].transform.childCount == 0)
            {
                SpawnItem(item, itemSlots[i].transform);
                return true;
            }
        }

        return false;
    }

    private void SpawnItem(GameObject item, Transform itemSlot)
    {
        item.transform.SetParent(itemSlot);
        item.transform.localScale = new Vector3Int(1, 1);
    }
}
