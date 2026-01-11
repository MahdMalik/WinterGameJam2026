using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    public int numItemsInInventory;

    Item[] inventory;

    int numSlotsFilled;

    int selectedSlot = 0;

    Transform[] slotObjects;

    public Sprite UnselectedSlotImage;
    public Sprite SelectedSlotImage;
    
    // Start is called before the first frame update
    void Awake()
    {
        // Debug.Log("I think awake is a lie");
        inventory = new Item[numItemsInInventory];
        numSlotsFilled = 0;
        Item.PlayerGotItem += OnItemGet;

        slotObjects = new Transform[numItemsInInventory];

        for(int i = 0; i < inventory.Length; i++)
        {
            slotObjects[i] = transform.Find($"Slot{i + 1}").GetChild(0);
            slotObjects[i].GetComponent<Image>().enabled = false;
        }
    }

    // Update is called once per frcame
    void Update()
    {
        
    }

    void OnItemGet(Item theItem)
    {
        for(int i = 0; i < inventory.Length; i++)
        {
            if(inventory[i] == null)
            {
                inventory[i] = theItem;
                slotObjects[i].GetComponent<Image>().sprite = inventory[i].image;
                slotObjects[i].GetComponent<Image>().enabled = true;
                break;
            }
        }
    }

    public bool IsInventoryFull()
    {
        return numItemsInInventory == numSlotsFilled;
    }

    // make it return an int so if there was an out of bounds issue, it'll return the corrected index to PlayerInventory
    public int SetItem(int index)
    {
        // assume we should wrap it around if we got an out of bounds number
        if(index >= numItemsInInventory)
        {
            index = 0;
        }
        else if(index < 0)
        {
            index = numItemsInInventory - 1;
        }
        
        //first change old one to unselected
        slotObjects[selectedSlot].parent.GetComponent<Image>().sprite = UnselectedSlotImage;
        // change the current one to the new
        selectedSlot = index;
        // now change the current one to the selected slot image
        slotObjects[selectedSlot].parent.GetComponent<Image>().sprite = SelectedSlotImage;

        return selectedSlot;
    }
}
