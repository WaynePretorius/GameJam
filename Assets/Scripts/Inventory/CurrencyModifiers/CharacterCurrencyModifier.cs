using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterCurrencyModifier : ScriptableObject
{
    public abstract void AddCurrency(GameObject character, float value);
}
