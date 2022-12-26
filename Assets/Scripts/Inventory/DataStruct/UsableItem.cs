using System.Collections.Generic;
using UnityEngine;

namespace InventorySpace.DataStructure
{   
    [CreateAssetMenu]
    public class UsableItem : Item, IDestroyableItem, IItemAction
    {
        [SerializeField] private List<ModifierData> modData = new List<ModifierData>();

        public string actionName => Tags.ACTION_USE;

        public AudioClip actionSFX { get; private set; }
        public bool PerFormAction(GameObject character, List<ItemParameter> itemStates = null)
        {
            foreach (ModifierData data in modData)
            {
                data.currencyModifier.AddCurrency(character, data.value);
            }
            return true;
        }
    }

    [System.Serializable]
    public class ModifierData
    {
        public CharacterCurrencyModifier currencyModifier;
        public float value;
    }
}