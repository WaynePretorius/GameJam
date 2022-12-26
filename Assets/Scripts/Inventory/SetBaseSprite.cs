using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBaseSprite : MonoBehaviour
{
    [SerializeField] private SpriteRenderer baseSprite;
    [SerializeField] private ItemTypes type;
 
    private EquiptSlotItem item;
    private ItemSlot slot;

    [SerializeField] private bool mustChange = false;
    [SerializeField] private bool isEmpty = true;

    private void Start()
    {
        item = GetComponent<EquiptSlotItem>();
        slot = GetComponent<ItemSlot>();
    }

    private void Update()
    {
        isEmpty = (slot.transform.childCount == 0)?true:false;

        if(isEmpty == false)
        {
            mustChange = true;
        }

        if(isEmpty && mustChange)
        {
            item.EquiptItem(baseSprite, type);
            mustChange = false;
        }
    }

}
