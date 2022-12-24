using UnityEngine.UI;
using TMPro;
using UnityEngine;

namespace InventorySpace.UI
{
    public class ItemDescription : MonoBehaviour
    {
        [SerializeField] private Image itemImage;
        [SerializeField] private TextMeshProUGUI titleText;
        //[SerializeField] private TextMeshProUGUI Description;

        private void Awake()
        {
            ResetDescription();
        }

        public void ResetDescription()
        {
            //deactivate the item and set it's text to nothing
            itemImage.gameObject.SetActive(false);
            titleText.text = "";
        }

        public void SetDescription(Sprite sprite, string itemName)
        {
            //activate the item, adding a sprite, and a name
            itemImage.gameObject.SetActive(true);
            itemImage.sprite = sprite;
            titleText.text = itemName;
        }
    }
}