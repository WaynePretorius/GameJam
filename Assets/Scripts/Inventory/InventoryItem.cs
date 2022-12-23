using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class InventoryItem : MonoBehaviour, IPointerClickHandler,IBeginDragHandler, IEndDragHandler,IDropHandler, IDragHandler
{
    //references
    [SerializeField] private Image itemSprite;
    [SerializeField] private TextMeshProUGUI quantityText;
    [SerializeField] private Image selectedImage;

    //states
    public event Action<InventoryItem> OnItemClicked, OnItemDropped, OnItemBeginDrag, OnItemEndDrag, OnRightMousButtonClicked;

    private bool isEmpty = true;

    private void Awake()
    {
        ResetData();
        Deselect();
    }

    //resets the item data in the inventory
    public void ResetData()
    {
        //set the sprite to false, it has no item
        this.itemSprite.gameObject.SetActive(false);
        isEmpty = true;
    }

    public void Deselect()
    {
        //deselect the current item
        selectedImage.enabled = false;
    }

    public void SetData(Sprite sprite, int quantity)
    {
        //set the data of the current item to that of the item it is holding
        this.itemSprite.gameObject.SetActive(true);
        this.itemSprite.sprite = sprite;
        this.quantityText.text = quantity + "";
        //the inventory slot is taken
        isEmpty = false;
    }

    public void Select()
    {
        //show that the item is selected
        selectedImage.enabled = true;
    }

    //events that handles the different mouse events to be used in the inventory

    public void OnDrop(PointerEventData eventData)
    {
        OnItemDropped?.Invoke(this);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnItemEndDrag?.Invoke(this);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isEmpty) { return; }
        OnItemBeginDrag?.Invoke(this);
    }

    public void OnPointerClick(PointerEventData pointerData)
    {
        if (pointerData.button == PointerEventData.InputButton.Right)
        {
            OnRightMousButtonClicked?.Invoke(this);
        }
        else
        {
            OnItemClicked?.Invoke(this);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }
}
