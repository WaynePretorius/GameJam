using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerResources : MonoBehaviour
{
    //variables
    [SerializeField] private int currentApples = 0;
    [SerializeField] private int baseHarvestAmount = 2;
    [SerializeField] private int harvestAmount = 2;
    [SerializeField] private int coins = 0;

    //references
    [SerializeField] private TextMeshProUGUI appleText;
    [SerializeField] private TextMeshProUGUI coinText;
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

    public int Coins
    {
        get { return coins; }
        set { coins = value; }
    }

    //public functions
    public void AddCoins(int addedCoins)
    {
        coins += addedCoins;
    }

    public void RemoveCoins(int coinsRemoved)
    {
        coins -= coinsRemoved;
    }

    public void SetHarvestAmount(int harvesAmountAdded)
    {
        harvestAmount = harvesAmountAdded;
    }

    public void ExchangeApplesForCoins()
    {
        int price = FindObjectOfType<Apples>().GetApplePrice();
        if(currentApples > 0)
        {
            int amount = currentApples;
            for (int i = 0; i < amount; i++)
            {
                coins += price;
            }
            currentApples = 0;
        }
    }

    private void Update()
    {
        ShowResources();
    }

    //shows the resources that the player currently has
    private void ShowResources()
    {
        //shows the amount of apples
        appleText.text = currentApples.ToString();
        //shows the amount of coins
        coinText.text = coins.ToString();
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
            //if it is an apple, then it can be harvested
            if (targetObject.gameObject.name.Contains(Tags.OBJNAME_APPLE))
            {
                //get the target apple component and harves, while adding the apples to the character
                Apples targetApple = targetObject.GetComponent<Apples>();
                currentApples += targetApple.collectApples(harvestAmount);
            }
        }
    }
}
