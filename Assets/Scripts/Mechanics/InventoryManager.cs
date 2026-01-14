using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public int numItemsInInventory;

    Item[] inventory;

    int numSlotsFilled;

    int selectedSlot = 0;

    Transform[] slotObjects;

    public Sprite UnselectedSlotImage;
    public Sprite SelectedSlotImage;

    public static event Action itemsRemoved;
    
    // Awake is called before start, which we need in this case to make sure it runs first so the sword can get added here
    void Awake()
    {
        // Debug.Log("I think awake is a lie");
        inventory = new Item[numItemsInInventory];
        numSlotsFilled = 0;

        // this gets a slot of UI objects we'll be modifying more than once
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

    //run this when we get an item. We know already that the inventory isn't full when we run this funciton, so we
    // dont' have to worry about that
   public void AddItem(Item theItem)
    {
        for(int i = 0; i < inventory.Length; i++)
        {
            // once we find the first empty spot
            if(inventory[i] == null)
            {
                inventory[i] = theItem;
                //set sprite as well in the UI
                slotObjects[i].GetComponent<Image>().sprite = inventory[i].image;
                slotObjects[i].GetComponent<Image>().enabled = true;
                numSlotsFilled++;
                break;
            }
        }
    }

    // checks if the inventory is at max capacity, so no more items can be added
    public bool IsInventoryFull()
    {
        return numItemsInInventory == numSlotsFilled;
    }

    // sets the current inventory slot number as what's currently being selected
    public void SetSelectedItem(int index)
    {
        // assume we should wrap it around if we got out of bounds
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
    }

    // this is just called when we want to move onto the next ivnentory slot to the right. We can't just pass this into
    // the function "SetSelectedItem" because the class calling that function doesn't know the current idnex, so we make 
    // this helper function and the one below for when we move to hte left.
    public void IncrementSelectedItem()
    {
        SetSelectedItem(selectedSlot + 1);
    }

    public void DecrementSelectedItem()
    {
        SetSelectedItem(selectedSlot - 1);
    }

    // removes the item from inventory
    public Item RemoveItem(int index)
    {
        Item removedItem = inventory[index];
        // they may have tried to remove something in an empty slot, be wary of that
        if(removedItem == null)
        {
            return null;
        }
        // now actually remove the item
        numSlotsFilled--;
        inventory[index] = null;
        slotObjects[index].GetComponent<Image>().enabled = false;

        return removedItem;
    }

    // by default if called without the index, we remove the current slot seelected
    public Item RemoveItem()
    {
        return RemoveItem(selectedSlot);
    }

    // resets entire inventory, like on death
    private void ResetInventory()
    {
        // goes through every item and removes it cyclically, by starting at the selected item and moving
        // to the right. Make sure to account for wraparound too
        for(int i = 0; i < numItemsInInventory; i++)
        {
            RemoveItem();
            selectedSlot++;
            if(selectedSlot == numItemsInInventory)
            {
                selectedSlot = 0;
            }
        }
        Debug.Log($"FINAL INDEX: {selectedSlot}");

        // once all the items are official removed, emit this so all the items can reset themsevles individually
        itemsRemoved.Invoke();
    }

    // activates the selected item so long as the slot is non empty and its available
    public void ActivateItem()
    {
        if(inventory[selectedSlot] != null && !inventory[selectedSlot].getCooldown() && !inventory[selectedSlot].getActivated())
        {
            inventory[selectedSlot].Activate();   
        }
    }

}
