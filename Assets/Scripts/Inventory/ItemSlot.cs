using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public ItemTypes allowedItemTypes;

    public bool isEmpty = true;

    [SerializeField] private bool isEquipable = false;
    [SerializeField] private GameObject sellButton;
    [SerializeField] private TextMeshProUGUI sellText;

    private bool inShop = false;
    public bool isSellButton = false;
    public bool isSold = false;

    public bool SetInShop = true;

    private void OnEnable()
    {
        if (isSellButton)
        {
            inShop = FindObjectOfType<ShopInventory>().gameObject.activeInHierarchy;

            if (inShop)
            {
                sellButton.SetActive(true);
                sellText.gameObject.SetActive(true);
            }
            else
            {
                sellButton.SetActive(false);
                sellText.gameObject.SetActive(false);
            }
        }
    }


    public GameObject item
    {
        //fits child of the item slot is the image that will be dragged
        get
        {
            if(transform.childCount != 0)
            {
                isEmpty = false;
                return transform.GetChild(0).gameObject;
            }
            return null;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {

            if (item == null)
            {
                //if the slot is empty, item can be dropped
                GameObject draggedItem = DragDropScript.draggedItem;

                EquiptSlotItem script = GetComponent<EquiptSlotItem>();

                //compare itemtype to dragged item
                ItemTypes itemDraggedType = draggedItem.GetComponent<DragDropScript>().itemType;

                if (IsTypeCorrect(itemDraggedType))
                {
                    draggedItem.transform.SetParent(transform);
                    if (isEquipable)
                    {
                        if (script != null)
                        {
                            script.EquiptItem(draggedItem, itemDraggedType);
                            if (IsHarvestType(itemDraggedType))
                            {
                                FindObjectOfType<PlayerResources>().SetHarvestAmount(draggedItem.GetComponent<DragDropScript>().GetHarvestAmount);
                            }
                        }
                    }
                }
            }
            else
            {
                //if there is other items in the slot
                GameObject draggedItem = DragDropScript.draggedItem;
                ItemTypes itemDraggedType = draggedItem.GetComponent<DragDropScript>().itemType;

                if (IsTypeCorrect(itemDraggedType))
                {
                    if (isEquipable)
                    {
                        EquiptSlotItem script = GetComponent<EquiptSlotItem>();
                        if (script != null)
                        {
                            script.EquiptItem(draggedItem, itemDraggedType);
                            if (IsHarvestType(itemDraggedType))
                            {
                                FindObjectOfType<PlayerResources>().SetHarvestAmount(draggedItem.GetComponent<DragDropScript>().GetHarvestAmount);
                            }
                        }
                    }



                    item.transform.SetParent(draggedItem.transform.parent);
                    draggedItem.transform.SetParent(transform);
                }
            }
        
    }

    public void SetBoolFalse()
    {
        StartCoroutine(SetSoldFalse());
    }

    private IEnumerator SetSoldFalse()
    {
        yield return new WaitForSeconds(1f);
        isSold = false;
    }

    private bool IsTypeCorrect(ItemTypes item)
    {
        if(allowedItemTypes == ItemTypes.Any || allowedItemTypes == ItemTypes.Sell)
        {
            return true;
        }
        else
        {
            return allowedItemTypes == item;
        }
    }

    private bool IsHarvestType(ItemTypes item)
    {
        if(item == ItemTypes.Harvester)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
