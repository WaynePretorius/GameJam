using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class ItemDescription : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI titleText;

    private void Awake()
    {
        ResetDescription();
    }

    public void ResetDescription()
    {
        //deactivate the item and set it's text to nothing
        this.itemImage.gameObject.SetActive(false);
        this.titleText.text = "";
    }

    public void SetDescription(Sprite sprite, string itemName)
    {
        //activate the item, adding a sprite, and a name
        this.itemImage.gameObject.SetActive(true);
        this.itemImage.sprite = sprite;
        this.titleText.text = itemName;
    }
}
