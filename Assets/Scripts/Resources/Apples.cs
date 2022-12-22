using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apples : MonoBehaviour
{
    //variables
    [SerializeField] private int minApples;
    [SerializeField] private int maxApples;
    [SerializeField] private int showMinApples;
    [SerializeField] private int showlittleApples;
    [SerializeField] private int showlotsApples;
    [SerializeField] private int showMaxApples;

    [SerializeField]private int apples;

    //references
    [SerializeField] private Sprite minApplesSprite;
    [SerializeField] private Sprite littleApplesSprite;
    [SerializeField] private Sprite lotsApplesSprite;
    [SerializeField] private Sprite maxApplesSprite;
    [SerializeField] private Item item;

    private SpriteRenderer mySprite;

    //states
    private bool hasApples;

    //getters setters
    public bool HasApples
    {
        get { return hasApples; }
        set { hasApples = value; }
    }

    public void CreateApples()
    {
        apples = Random.Range(minApples, maxApples);
        SetAppleSprite();
        hasApples = true;
    }

    private void Start()
    {
        mySprite = GetComponent<SpriteRenderer>();
        //when the resource is spawned, give it a random amount
        apples = Random.Range(minApples, maxApples);
        hasApples = true;
        mySprite.sprite =  SetAppleSprite();
    }

    //return the amount of apples collected
    public int collectApples(int collectApples)
    {
        Debug.Log("removeing apples " + collectApples.ToString());
        //if the amount of apples are less than what can be collected
        if(collectApples > apples)
        {
            //the amount of apples will be only what is left
            collectApples = apples;
            //set the apples to zero
            apples = 0;
            //the resource is finished
            hasApples = false;
            SetAppleSprite();
        }
        else
        {
            //deduct the amount of apples collected
            apples -= collectApples;
            SetAppleSprite();
        }

        //return the amount of apples collected
        return collectApples;
    }

    //Set the sprite of the apples
    private Sprite SetAppleSprite()
    {
        Sprite appleSprite;

        //Depending on how many apples there are, the sprite will show accordingly
        if(apples <= 0)
        {
            appleSprite = null;
        }

        if(apples < showMinApples)
        {
            appleSprite = minApplesSprite;
        }
        else if(apples > showMinApples && apples < showlittleApples)
        {
            appleSprite = littleApplesSprite;
        }
        else if(apples > showlittleApples && apples < showlotsApples)
        {
            appleSprite = lotsApplesSprite;
        }
        else
        {
            appleSprite = maxApplesSprite;
        }

        return appleSprite;
    }
}
