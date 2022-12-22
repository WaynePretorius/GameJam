using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apples : MonoBehaviour
{
    //variables
    [SerializeField] private int minApples;
    [SerializeField] private int maxApples;

    private int apples;

    //references
    [SerializeField] private Sprite hasApplesSprite;
    [SerializeField] private Sprite noApples;


    //states
    private bool hasApples;

    //getters setters
    public bool HasApples
    {
        get { return hasApples; }
        set { hasApples = value; }
    }

    private void Start()
    {
        //when the resource is spawned, give it a random amount
        apples = Random.Range(minApples, maxApples);
        hasApples = true;
    }

    //return the amount of apples collected
    public int collectApples(int collectApples)
    {
        //if the amount of apples are less than what can be collected
        if(collectApples > apples)
        {
            //the amount of apples will be only what is left
            collectApples = apples;
            //set the apples to zero
            apples = 0;
            //the resource is finished
            hasApples = false;
        }
        else
        {
            //deduct the amount of apples collected
            apples -= collectApples;
        }

        //return the amount of apples collected
        return collectApples;
    }
}
