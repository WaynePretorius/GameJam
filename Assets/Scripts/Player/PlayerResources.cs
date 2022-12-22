using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerResources : MonoBehaviour
{
    //variables
    [SerializeField] private int currentApples = 0;
    [SerializeField] private int harvestAmount = 2;

    //references
    [SerializeField] private TextMeshProUGUI appleText;
    [SerializeField] private TextMeshProUGUI pressR;

    [SerializeField] GameObject targetObject;

    //states
    private bool canCollect = false;

    public bool CanCollect
    {
        get { return canCollect; }
    }

    //getters and Setters
    public int CurrentApples
    {
        get { return currentApples; }
        set { currentApples = value; }
    }

    private void Update()
    {
        ShowResources();
    }

    //shows the resources that the player currently has
    private void ShowResources()
    {
        //shoes the amount of apples
        appleText.text = currentApples.ToString();
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        //if the target is a collectable
        if(target.tag == Tags.TAGS_Collectable)
        {
            //if it contains the name apple
            if (target.gameObject.name.Contains(Tags.OBJNAME_APPLE))
            {
                //can collect
                canCollect = true;
                //set the target
                targetObject = target.gameObject;
                pressR.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D target)
    {
        //if the target is a collectable
        if (target.tag == Tags.TAGS_Collectable)
        {
            //if it contains the name apple
            if (target.gameObject.name.Contains(Tags.OBJNAME_APPLE))
            {
                //can't collect
                canCollect = false;
                //set the target
                targetObject = null;
                pressR.gameObject.SetActive(false);
            }
        }
    }

    public void GatherResource()
    {
        if(canCollect)
        {
            if (targetObject.gameObject.name.Contains(Tags.OBJNAME_APPLE))
            {
                Debug.Log("canharves");
                Apples targetApple = targetObject.GetComponent<Apples>();
                Debug.Log(targetApple.transform.position);
                currentApples += targetApple.collectApples(harvestAmount);
            }
        }
    }
}
