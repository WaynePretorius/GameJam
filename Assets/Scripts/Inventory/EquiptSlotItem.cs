using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquiptSlotItem : MonoBehaviour
{
    [SerializeField] private GameObject player;

    EquiptItemScript equiptItem;

    // Start is called before the first frame update
    private void Start()
    {
        SetReferences();
    }

    private void SetReferences()
    {
        player = FindObjectOfType<PlayerActions>().gameObject;
        equiptItem = player.GetComponent<EquiptItemScript>();
    }

    public void EquiptItem(GameObject itemToEquipt, ItemTypes itemType)
    {
        switch (itemType)
        {
            case ItemTypes.Hood :
                equiptItem.EquiptHood(itemToEquipt);
                break;
            case ItemTypes.Harvester:
                equiptItem.Equiptweapon(itemToEquipt);
                break;
            case ItemTypes.Shirt:
                equiptItem.EquiptShirt(itemToEquipt);
                break;
            case ItemTypes.Trousers:
                equiptItem.EquiptTrousers(itemToEquipt);
                break;
        }

    }    public void EquiptItem(SpriteRenderer itemToEquipt, ItemTypes itemType)
    {
        switch (itemType)
        {
            case ItemTypes.Hood :
                equiptItem.EquiptHood(itemToEquipt);
                break;
            case ItemTypes.Harvester:
                equiptItem.Equiptweapon(itemToEquipt);
                break;
            case ItemTypes.Shirt:
                equiptItem.EquiptShirt(itemToEquipt);
                break;
            case ItemTypes.Trousers:
                equiptItem.EquiptTrousers(itemToEquipt);
                break;
        }

    }

}
