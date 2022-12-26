using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ShopItemInfo : MonoBehaviour
{
    [SerializeField] private int itemPrice;
    [SerializeField] private Sprite itemSprite;

    public Sprite ItemSprite
    {
        get { return itemSprite; }
    }

    private void Start()
    {
        itemSprite = GetComponent<Image>().sprite;
    }

    public void BuyItem()
    {
        
    }
}
