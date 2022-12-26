using InventorySpace.DataStructure;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    [field: SerializeField] public int quantity { get; set; } = 1;

    [SerializeField] private float duration = 0.3f;

    [field: SerializeField] public Item InventorySystem { get; private set; }

    [SerializeField] private AudioSource soundAudio;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = InventorySystem.ItemImage;
    }

    //change later
    public void DestroyItem()
    {
        //add code to reference apple deduction as in apple.cs
        GetComponent<Collider2D>().enabled = false;
        StartCoroutine(AnimateItemPickup());
    }

    private IEnumerator AnimateItemPickup()
    {
        float currentTime = 0;
        //soundAudio.PlayOneShot();
        Vector3 startScale = transform.localScale;
        Vector3 endScale = Vector3.zero;
        while(currentTime < duration)
        {
            currentTime += Time.deltaTime;
            transform.localScale = Vector3.Lerp(startScale, endScale, currentTime / duration);
            yield return null;
        }
        Destroy(gameObject);
        
    }
}
