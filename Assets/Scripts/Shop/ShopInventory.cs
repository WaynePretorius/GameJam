using System;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class ShopInventory : MonoBehaviour
{
    [SerializeField] private List<DragDropScript> shopItems;
    [SerializeField] private List<Button> shopButtons;

    private void Start()
    {
        PrepareShopItems();
    }
    private void PrepareShopItems()
    {
        for (int i = 0; i < shopItems.Count; i++)
        {

        }
    }
}
