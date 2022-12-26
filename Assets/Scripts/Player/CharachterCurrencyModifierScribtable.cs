using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharachterCurrencyModifierScribtable : CharacterCurrencyModifier
{
    public override void AddCurrency(GameObject character, float value)
    {
        PlayerResources resources = character.GetComponent<PlayerResources>();
        if (resources != null)
        {
            resources.AddCoins((int)value);
        }
    }
}
