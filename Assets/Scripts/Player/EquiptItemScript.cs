using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class EquiptItemScript : MonoBehaviour
{
    [SerializeField] private SpriteRenderer torso;
    [SerializeField] private SpriteRenderer hood;
    [SerializeField] private SpriteRenderer trousers;
    [SerializeField] private SpriteRenderer harvester;

    public void EquiptShirt(GameObject shirt)
    {
        torso.sprite = shirt.GetComponent<Image>().sprite;
    }
    public void EquiptHood(GameObject head)
    {
        hood.sprite = head.GetComponent<Image>().sprite;
    }
    public void EquiptTrousers(GameObject pelvis)
    {
        trousers.sprite = pelvis.GetComponent<Image>().sprite;
    }
    public void Equiptweapon(GameObject weapon)
    {
        harvester.sprite = weapon.GetComponent<Image>().sprite;
    }    

}
