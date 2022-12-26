using System;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySpace.DataStructure
{
    public abstract class Item : ScriptableObject
    {
        //properties of the items
        [field: SerializeField] public bool isStackable { get; set; }
        public int ID => GetInstanceID();
        [field: SerializeField] public int MaxStackSize { get; set; }
        [field: SerializeField] public string Name { get; set; }
        [field: SerializeField] [field: TextArea] public string Description { get; set; }
        [field: SerializeField] public Sprite ItemImage { get; set; }
        [field: SerializeField] public List<ItemParameter> defaultParamter { get; set; }
    }

    public interface IDestroyableItem
    {

    }
    
    public interface IItemAction
    {
        public string actionName { get; }

        public AudioClip actionSFX { get; }

        bool PerFormAction(GameObject character, List<ItemParameter> itemStates);
    }

    [Serializable]
    public struct ItemParameter : IEquatable<ItemParameter>
    {
        public EquiptItemsScriptable itemParamater;
        public float value;

        public bool Equals(ItemParameter parameter)
        {
            return parameter.itemParamater == itemParamater;
        }
    }
}