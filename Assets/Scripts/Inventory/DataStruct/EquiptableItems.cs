using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySpace.DataStructure
{
    public class EquiptableItems : Item, IDestroyableItem, IItemAction
    {
        public string actionName => Tags.ACTION_EQUIPT;

        public AudioClip actionSFX {get; private set;}

        public bool PerFormAction(GameObject character, List<ItemParameter> itemStates)
        {
            throw new System.NotImplementedException();
        }
    }



}