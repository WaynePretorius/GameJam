using System;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySpace.UI
{
    public class InventoryPage : MonoBehaviour
    {
        //variables
        private int currentDraggedIndex = -1;

        //references
        [SerializeField] private RectTransform contentPanel;
        [SerializeField] private InventoryItem itemPrefab;
        [SerializeField] private MouseFollower follower;

        private List<InventoryItem> itemList = new List<InventoryItem>();

        //states & events
        public event Action<int> OnDescriptionRequested, OnItemRequested, OnStartDragging;

        public event Action<int, int> OnSwapItems;

        private void Awake()
        {
            ShowInventory(false);
            //toggle for the mouse follow item
            follower.Toggle(false);
        }

        public void InitializeInventory(int inventorySize)
        {
            //initliazes the inventory slots to the size that is given
            for (int i = 0; i < inventorySize; i++)
            {
                //creates the inventory slot, sets the scale to 1, at the current position of the panel, and sets it as the child so it is on the grid
                InventoryItem item = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
                item.transform.SetParent(contentPanel);
                item.transform.localScale = new Vector3(1, 1, 1);
                itemList.Add(item);

                //adds events to the slot so it can handle the input of the player
                item.OnItemClicked += HandleSelection;
                item.OnItemBeginDrag += HandleBeginDrag;
                item.OnItemDropped += HandleSwap;
                item.OnItemEndDrag += HandleEndDrag;

            }
        }

        //resets the data of the inventory
        internal void ResetAllItems()
        {
            //for all the items in the list
            foreach (var item in itemList)
            {
                //reset and deselect
                item.ResetData();
                item.Deselect();
            }
        }

        //updates the description of the item(for shopkeeper inventory)
        internal void UpdateDescription(int itemIndex, Sprite itemImage, string name, string description)
        {
            Debug.Log("You have selected item description for " + name + " DESCRIPTION: " + description);
            //deselect all items
            DeselectAllItems();
            //select the item and display the description
            itemList[itemIndex].Select();
        }

        //update the inventory slot according to the item data
        public void UpdateData(int itemIndex, Sprite itemImage, int itemQuantity)
        {
            //if there is more items than in the index
            if (itemList.Count > itemIndex)
            {
                //set the data of the item at that index
                itemList[itemIndex].SetData(itemImage, itemQuantity);
            }
        }

        private void HandleEndDrag(InventoryItem invenItem)
        {
            ResetDraggedItem();
        }

        private void HandleSwap(InventoryItem invenItem)
        {
            //add the index of the item and store it
            int index = itemList.IndexOf(invenItem);
            if (index == -1)
            {
                return;
            }
            //swap the items if they can be swapped
            OnSwapItems?.Invoke(currentDraggedIndex, index);
            HandleSelection(invenItem);
        }

        private void ResetDraggedItem()
        {
            //deactivate the mouse follower item so it doesn't show
            follower.Toggle(false);
            //-1 does not get picked up as part of the list
            currentDraggedIndex = -1;
        }

        private void HandleBeginDrag(InventoryItem invenItem)
        {
            //get the index of the current item
            int index = itemList.IndexOf(invenItem);
            if (index == -1) { return; }
            //pick up the item and display it in the follower item slot
            currentDraggedIndex = index;
            HandleSelection(invenItem);
            OnStartDragging?.Invoke(index);
        }

        public void CreateDraggedItem(Sprite sprite, int quantity)
        {
            //set the sprite for the inventory item to the mouse follower item and activate it
            follower.Toggle(true);
            follower.SetData(sprite, quantity);
        }

        private void HandleSelection(InventoryItem invenItem)
        {
            int index = itemList.IndexOf(invenItem);
            if (index == 1) { return; }
        }

        public void ShowInventory(bool active)
        {
            //activates and deactivates the inventory panel
            gameObject.SetActive(active);
            if (active)
            {
                ResetSelection();
            }
            else
            {
                ResetDraggedItem();
            }
        }

        public void ResetSelection()
        {
            DeselectAllItems();
        }

        private void DeselectAllItems()
        {
            //deselect all the items in the inventory
            foreach (InventoryItem item in itemList)
            {
                item.Deselect();
            }
        }
    }
}