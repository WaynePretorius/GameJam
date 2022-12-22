using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item" ,menuName = "Item/Create")]
public class Item : ScriptableObject
{
    public int id;
    public int price;

    public string itemName;

    public Sprite icon;
}
