using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollower : MonoBehaviour
{
    //references
    private Canvas canvas;
    private InventoryItem item;

    public void Awake()
    {
        //cache the references
        canvas = transform.root.GetComponent<Canvas>();
        item = GetComponentInChildren<InventoryItem>();
    }

    public void SetData(Sprite sprite, int quantity)
    {   
        //set the data of the sprite and quantity for the mouse follower
        item.SetData(sprite, quantity);
    }

    private void Update()
    {
        //reference the position of the follower
        Vector2 pos;
        //using the canvas, working in screenspace, get the mouse position and give it a vector for the current position
        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)canvas.transform, Input.mousePosition, canvas.worldCamera, out pos);
        //move the object to the mouse pointer
        transform.position = canvas.transform.TransformPoint(pos);
    }

    public void Toggle(bool val)
    {
        //activate or deactivates the follower
        gameObject.SetActive(val);
    }

}
