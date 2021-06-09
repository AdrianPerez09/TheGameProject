using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    private bool inventoryEnabled;

    public GameObject inventory;

    private int allSlots;
    private int enabledSlots;
    private GameObject[] slot;

    public GameObject slotHandler;

    void Start()
    {
        allSlots = slotHandler.transform.childCount;

        slot = new GameObject[allSlots];

        for(int i = 0; i < allSlots; i++)
        {
            slot[i] = slotHandler.transform.GetChild(i).gameObject;
        }
    }


    void Update()
    {

        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryEnabled = !inventoryEnabled;

        }

        if (inventoryEnabled)
        {
            inventory.SetActive(true);
        }
        else{


            inventory.SetActive(false);
        }

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag=="item")
        {
            GameObject itemPickedUp = other.gameObject;

            Item item = itemPickedUp.GetComponent<Item>();

            Additem(itemPickedUp,item.ID,item.type,item.descripcion,item.icon);

        }

    }

    public void Additem(GameObject itemObject,int itemID,string itemType,string itemDescription, Sprite itemIcon)
    {
        
        for(int i = 0; i < allSlots; i++)
        {


        }

    }
}
