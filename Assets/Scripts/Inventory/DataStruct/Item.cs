using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    //properties of the items
    [field: SerializeField] public bool isStackable { get; set; }
    public int ID => GetInstanceID();
    [field: SerializeField] public int MaxStacSize { get; set; }
    [field: SerializeField] public string Name { get; set; }
    [field: SerializeField][field: TextArea] public string Description { get; set; }
    [field: SerializeField] public Sprite ItemImage { get; set; }
}
