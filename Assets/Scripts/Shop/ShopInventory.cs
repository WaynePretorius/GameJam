using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ShopInventory : MonoBehaviour
{
    private int totalPrice = 0;

    [SerializeField] private List<ShopItemInfo> shopItems;
    [SerializeField] private List<GameObject> itemsToBuy;
    [SerializeField] private TextMeshProUGUI totalText;
    [SerializeField] private Button appleButton;

    public int TotalPrice
    {
        get { return totalPrice; }
        set { totalPrice = value; }
    }

    private void OnEnable()
    {
        PrepInventory();

    }

    public void PrepInventory()
    {
        totalText.text = totalPrice.ToString();
        if(FindObjectOfType<PlayerResources>().CurrentApples > 0)
        {
            appleButton.interactable = true;
        }
        else
        {
            appleButton.interactable = false;
        }
    }

    public void AddReduceItems()
    {
        itemsToBuy = new List<GameObject>();
        for (int i = 0; i < shopItems.Count; i++)
        {
            if (shopItems[i].IsSelected == true)
            {
                itemsToBuy.Add(shopItems[i].SelectedItem());
            }
        }
    }

    public void SellApples()
    {
        FindObjectOfType<PlayerResources>().ExchangeApplesForCoins();
        appleButton.interactable = false;
    }

    public void BuyButton()
    {
        BuyItem(itemsToBuy);
        ResetSelection();
    }

    private void ResetSelection()
    {
        foreach (var item in shopItems)
        {
            item.SetSelected();
        }
        totalPrice = 0;
        PrepInventory();
    }

    public void BuyItem(List<GameObject> items)
    {
        if(FindObjectOfType<PlayerResources>().Coins >= totalPrice)
        {
            foreach (var item in items)
            {
                GameObject newitem = Instantiate(item);
                FindObjectOfType<InventoryManager>().AddItem(newitem);
            }
            FindObjectOfType<PlayerResources>().RemoveCoins(totalPrice);
        }
    }
}
