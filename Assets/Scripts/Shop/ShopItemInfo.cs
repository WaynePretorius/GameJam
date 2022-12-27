using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class ShopItemInfo : MonoBehaviour
{
    [SerializeField] private int itemPrice;
    [SerializeField] private DragDropScript item;
    [SerializeField] private Image ItemImage;
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite selectedSprite;  

    private TextMeshProUGUI priceText;
    private ShopInventory inventory;

    private bool isSelected = false;

    public int GetPrice
    {
        get { return itemPrice; }
    }

    public void SetSelected()
    {
        isSelected = false;
        GetComponent<Image>().sprite = normalSprite;
    }

    public bool IsSelected
    {
        get { return isSelected; }
    }
        
    private void Start()
    {
        PrepareShopButton();
    }

    private void PrepareShopButton()
    {
        priceText = GetComponentInChildren<TextMeshProUGUI>();
        priceText.text = itemPrice.ToString();
        ItemImage.sprite = item.GetComponent<Image>().sprite;
        inventory = FindObjectOfType<ShopInventory>();
    }

    public void SelectItem()
    {
        isSelected = !isSelected;
        if (IsSelected)
        {
            inventory.TotalPrice += itemPrice;
            inventory.PrepInventory();
            inventory.AddReduceItems();
            GetComponent<Image>().sprite = selectedSprite;
        }
        else
        {
            inventory.TotalPrice -= itemPrice;
            inventory.PrepInventory();
            inventory.AddReduceItems();
            GetComponent<Image>().sprite = normalSprite;
        }
    }

    public GameObject SelectedItem()
    {
            return item.gameObject;
    }
}
