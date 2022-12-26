using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDropScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    //references
    public static GameObject draggedItem;

    public ItemTypes itemType;

    Vector3 startPos;

    Transform startParent;

    /// <summary>
    /// start the dragging of the object
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        draggedItem = gameObject;
        startPos = transform.position;
        startParent = transform.parent;
        //stop the interation with other objects
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    //drag the item around
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    //drop the item at the location or itemslot
    public void OnEndDrag(PointerEventData eventData)
    {
        draggedItem = null;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        if (transform.parent == startParent)
        {
            transform.position = startPos;
        }
    }

}
