using InventorySpace.DataStructure;
using UnityEngine;

public class PickupSystem : MonoBehaviour
{
    [SerializeField] private Inventory inventoryData;

    private void OnTriggerEnter2D(Collider2D target)
    {
        ItemScript item = target.GetComponent<ItemScript>();

        if(item != null)
        {
            int remainder = inventoryData.AddItem(item.InventorySystem, item.quantity);
            if(remainder == 0)
            {
                item.DestroyItem();
            }
            else
            {
                item.quantity = remainder;
            }
        }
    }
}
