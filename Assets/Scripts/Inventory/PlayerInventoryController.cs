using InventorySpace.UI;
using InventorySpace.DataStructure;
using UnityEngine;
using System.Collections.Generic;
using System.Text;

namespace InventorySpace
{
    public class PlayerInventoryController : MonoBehaviour
    {
        //references
        [SerializeField] private InventoryPage inventoryUI;
        [SerializeField] private Inventory inventoryData;

        //Remove after testing
        public List<InventoryItemScribatable> initialItems = new List<InventoryItemScribatable>();

        // Start is called before the first frame update
        void Start()
        {
            //initialize the inventory
            PrepareUi();
            PrepareInventoryData();
        }

        private void PrepareInventoryData()
        {
            //initialize the inventory data
            inventoryData.Initialize();
            //cal the event that the data has changed
            inventoryData.OnInventoryUpdated += UpdateInventoryUI;
            foreach (InventoryItemScribatable item in initialItems)
            {
                //if there is no item in the list, skip the rest of the loop and start again
                if (item.IsEmpty)
                {
                    continue;
                }
                //add the data to the inventory slot
                inventoryData.AddItem(item);
            }
        }

        private void UpdateInventoryUI(Dictionary<int, InventoryItemScribatable> inventoryState)
        {
            //reset all inventory items
            inventoryUI.ResetAllItems();
            //for all the items in the stored dictionary(keyvalupairs)
            foreach (var item in inventoryState)
            {
                //update the Ui of the inventory
                inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
            }
        }

        private void PrepareUi()
        {
            inventoryUI.InitializeInventory(inventoryData.Size);
            inventoryUI.OnDescriptionRequested += HandleDescriptionRequested;
            inventoryUI.OnSwapItems += HandleSwapItems;
            inventoryUI.OnStartDragging += HandleDragging;
            inventoryUI.OnItemRequested += HandleItemActionRequested;
        }

        private void HandleItemActionRequested(int itemIndex)
        {
            InventoryItemScribatable item = inventoryData.GetItemAt(itemIndex);
            if (item.IsEmpty)
            {
                return;
            }

            IItemAction itemAction = item.item as IItemAction;
            if(itemAction != null)
            {
                itemAction.PerFormAction(gameObject, null);
            }

            IDestroyableItem RemoveItem = item.item as IDestroyableItem;
            if(RemoveItem != null)
            {
                inventoryData.RemoveItem(itemIndex, 1);
            }
        }

        private void HandleDragging(int itemIndex)
        {
            //sets the item and gets the info 
            InventoryItemScribatable item = inventoryData.GetItemAt(itemIndex);
            
            //if the slot is empty, skip the rest of the codeblock
            if (item.IsEmpty) 
            {
                return; 
            }

            //set the followers icon to the current selected item
            inventoryUI.CreateDraggedItem(item.item.ItemImage, item.quantity);
        }

        private void HandleSwapItems(int itemIndex1, int itemIndex2)
        {
            //swap the items that is selected and the item that is in the current slot
            inventoryData.SwapItems(itemIndex1, itemIndex2);
        }

        private void HandleDescriptionRequested(int itemIndex)
        {
            //get the data of the item at the given location
            InventoryItemScribatable inventoryItem = inventoryData.GetItemAt(itemIndex);
            
            //if the slot is empty, disable the selected block, skip the rest of the code block
            if (inventoryItem.IsEmpty)
            {
                inventoryUI.ResetSelection();
                return;
            }
            //update the UI with the image, name and description of the image
            Item item = inventoryItem.item;

            string description = PrepareDescription(inventoryItem);

            inventoryUI.UpdateDescription(itemIndex, item.ItemImage, item.name, description);
        }

        public string PrepareDescription(InventoryItemScribatable inventoryItem)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(inventoryItem.item.Description);
            sb.AppendLine();
            for (int i = 0; i < inventoryItem.parameters.Count; i++)
            {
                sb.Append($"{inventoryItem.parameters[i].itemParamater} " + $": {inventoryItem.parameters[i].value} / " + $" {inventoryItem.item.defaultParamter[i].value}");
                sb.AppendLine();
            }
            return sb.ToString();
        }

        private void Update()
        {
            //if the inventory page is active
            if (inventoryUI.isActiveAndEnabled)
            {
                foreach (var item in inventoryData.GetCurrenInventoryState())
                {
                    //update the data for every item in the inventory
                    inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
                }
            }
        }

    }
}