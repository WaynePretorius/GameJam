using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySpace.DataStructure
{
    [CreateAssetMenu]
    public class EquiptItemsScriptable : ScriptableObject
    {

        [field: SerializeField] public string ParameterName { get; private set; }

    }
}
