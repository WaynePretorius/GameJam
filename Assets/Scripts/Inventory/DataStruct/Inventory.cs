using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;


namespace InventorySpace.DataStructure
{
    [CreateAssetMenu]
    public class Inventory : ScriptableObject
    {
        //properite variables
        [field: SerializeField] public int Size { get; private set; } = 10;

        //references
        [SerializeField] private List<InventoryItemScribatable> inventoryItems;

        //States and events
        public event Action<Dictionary<int, InventoryItemScribatable>> OnInventoryUpdated;

        //initialize the invenotry
        public void Initialize()
        {
            //set a new list of items on the start, when the inventory is opened
            inventoryItems = new List<InventoryItemScribatable>();
            for (int i = 0; i < Size; i++)
            {
                //add a inventory slot until it is met with the same size as declared
                inventoryItems.Add(InventoryItemScribatable.GetEmptyItem());
            }
        }

        public int AddItem(Item item, int quantity, List<ItemParameter> parameter = null)
        {
            //if the item can't be stacked
            if (item.isStackable == false)
            {
                    //as long as the quantity is more than 0 and the inventory as empty slots
                    while (quantity > 0 && IsInventoryFull() == false)
                    {
                        //add the item, only 1 of them
                        quantity -= AddItemToFreeSlot(item, 1, parameter);
                    }
                    InformAboutChange();
                    return quantity;
            }
            //quantity is same is item quantity
            quantity = AddStackableItem(item, quantity);
            InformAboutChange();
            return quantity;
        }
        /// <summary>
        /// Add the item to a free slot as long as the slot is empty
        /// </summary>
        /// <param name="item">item that is given</param>
        /// <param name="quantity">quantity that comes with the item, non and stacakble</param>
        /// <returns></returns>
        private int AddItemToFreeSlot(Item item, int quantity, List<ItemParameter> parameter = null)
        {
            InventoryItemScribatable newItem = new InventoryItemScribatable
            {
                item = item,
                quantity = quantity,
                parameters = new List<ItemParameter>(parameter == null?item.defaultParamter : parameter)
            };

            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmpty)
                {
                    inventoryItems[i] = newItem;
                    return quantity;
                }
            }
            return 0;
        }

        public void RemoveItem(int itemIndex, int value)
        {
            if(inventoryItems.Count > itemIndex)
            {
                if (inventoryItems[itemIndex].IsEmpty) 
                {
                    return; 
                }
                int remainder = inventoryItems[itemIndex].quantity - value;
                if(remainder <= 0)
                {
                    inventoryItems[itemIndex] = InventoryItemScribatable.GetEmptyItem();
                }
                else
                {
                    inventoryItems[itemIndex] = inventoryItems[itemIndex].ChangeQuantity(remainder);
                }

                InformAboutChange();
            }
        }

        /// <summary>
        /// Look at all the inventory slots, if the are empty, return false otherwise returns true if there is any items in the slot
        /// </summary>
        /// <returns>if the inventory slots are full</returns>
        private bool IsInventoryFull() => inventoryItems.Where(item => item.IsEmpty).Any() == false;

        /// <summary>
        /// Add the stackable items together.  If the items has reached it's max stacksize, create a new item in the inventory as lon as it is not full, otherwise the rest of the items have to be dropped to the ground
        /// </summary>
        /// <param name="item"></param>
        /// <param name="quantity"></param>
        /// <returns>quantity</returns>
        private int AddStackableItem(Item item, int quantity)
        {
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmpty) { continue; }
                if(inventoryItems[i].item.ID == item.ID)
                {
                    int amountCanTake = inventoryItems[i].item.MaxStackSize - inventoryItems[i].quantity;

                    if(quantity > amountCanTake)
                    {
                        inventoryItems[i] = inventoryItems[i].ChangeQuantity(inventoryItems[i].item.MaxStackSize);
                        quantity -= amountCanTake;
                    }
                    else
                    {
                        inventoryItems[i] = inventoryItems[i].ChangeQuantity(inventoryItems[i].quantity + quantity);
                        InformAboutChange();
                        return 0;
                    }
                }
            }

            while(quantity > 0 && IsInventoryFull() == false)
            {
                int newQuantity = Mathf.Clamp(quantity, 0, item.MaxStackSize);
                quantity -= newQuantity;
                AddItemToFreeSlot(item, newQuantity);
            }
            return quantity;
        }

        /// <summary>
        /// add item to the slot
        /// </summary>
        /// <param name="item"></param>
        public void AddItem(InventoryItemScribatable item)
        {
            AddItem(item.item, item.quantity);
        }

        /// <summary>
        /// stores the state of the inventory at the particular index, and declares if there is no item, the slot is empty
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, InventoryItemScribatable> GetCurrenInventoryState()
        {
            Dictionary<int, InventoryItemScribatable> returnValue = new Dictionary<int, InventoryItemScribatable>();
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmpty)
                {
                    continue;
                }
                returnValue[i] = inventoryItems[i];
            }

            return returnValue;
        }

        //returns the item that is at that particular index
        public InventoryItemScribatable GetItemAt(int itemIndex)
        {
            return inventoryItems[itemIndex];
        }

        /// <summary>
        /// swap the items from one index to the other if the player wishes to re arrange the inventory
        /// </summary>
        /// <param name="itemIndex1">1st item</param>
        /// <param name="itemIndex2">2nd item</param>
        public void SwapItems(int itemIndex1, int itemIndex2)
        {
            InventoryItemScribatable item1 = inventoryItems[itemIndex1];
            inventoryItems[itemIndex1] = inventoryItems[itemIndex2];
            inventoryItems[itemIndex2] = item1;
            InformAboutChange();
        }

        //call the event only if changes has been made and update the inventory
        private void InformAboutChange()
        {
            OnInventoryUpdated?.Invoke(GetCurrenInventoryState());
        }
    }

    //Item that is listed in the inventory
    [System.Serializable]
    public struct InventoryItemScribatable
    {
        public int quantity;
        public Item item;
        public List<ItemParameter> parameters;

        public bool IsEmpty => item == null;

        public InventoryItemScribatable ChangeQuantity(int newQuantity)
        {
            return new InventoryItemScribatable
            {
                item = this.item,
                quantity = newQuantity,
                parameters = new List<ItemParameter>(this.parameters)
            };
        }

        public static InventoryItemScribatable GetEmptyItem()
            => new InventoryItemScribatable
            {
                item = null,
                quantity = 0,
                parameters = new List<ItemParameter>()
            };
    }
}
