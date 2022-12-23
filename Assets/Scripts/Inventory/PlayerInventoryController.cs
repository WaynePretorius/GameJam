using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryController : MonoBehaviour
{
    //variables
    private int noOfSlots = 10;

    //referneces
    [SerializeField] private InventoryPage inventory;

    //getter and setter
    public int NoOfSlots
    {
        get { return noOfSlots; }
        set { noOfSlots = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        //initialize the inventory
        inventory.InitializeInventory(noOfSlots);
    }

}
