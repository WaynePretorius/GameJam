using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField]private List<ItemSlot> itemSlots = new List<ItemSlot>();

    private void Start()
    {
        AddSlotsToList();
    }

    private void AddSlotsToList()
    {
        ItemSlot[] slotsInBackpack;
        slotsInBackpack = GetComponentsInChildren<ItemSlot>();

        foreach (ItemSlot slot in slotsInBackpack)
        {
            itemSlots.Add(slot);
        }
    }

    public bool AddItem(GameObject item)
    {
        for (int i = 0; i < itemSlots.Count; i++)
        {
            if (itemSlots[i].transform.GetChild(0) == null)
            {
                if (itemSlots[i].isEmpty)
                {
                    item.transform.SetParent(itemSlots[i].transform);
                    return true;
                }
            }
        }

        return false;
    }
}
