using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public List<Item> Items = new List<Item>();

    public Transform itemContent;
    public GameObject inventoryItem;

    private void Awake()
    {
        Instance = this;
    }

    public void AddItem(Item item)
    {
        Items.Add(item);
    }

    public void RemoveItem(Item item)
    {

        Items.Remove(item);

    }

    public void ListItems()
    {
        foreach(Transform item in itemContent)
        {
            Destroy(item.gameObject);
        }

        foreach (var items in Items) 
        {
            GameObject inventoryOBJ = Instantiate(inventoryItem, itemContent);

            var itemIcon = inventoryOBJ.transform.Find(Tags.INVENTORY_OBJ_SPRITE).transform.GetComponent<SpriteRenderer>();
            var ItemName = inventoryOBJ.transform.Find(Tags.INVENTORY_OBJ_AMOUNT).GetComponent<TextMeshProUGUI>();

            itemIcon.sprite = items.icon;
            ItemName.text = items.itemName;
        }
    }
}
