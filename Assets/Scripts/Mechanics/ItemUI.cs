using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    public int numItemsInInventory;

    Item[] inventory;

    int numSlotsFilled;
    
    // Start is called before the first frame update
    void Awake()
    {
        // Debug.Log("I think awake is a lie");
        inventory = new Item[4];
        numSlotsFilled = 0;
        Item.PlayerGotItem += OnItemGet;

        for(int i = 0; i < inventory.Length; i++)
        {
            Transform child = transform.Find($"Slot{i + 1}").GetChild(0);
            child.GetComponent<Image>().enabled = false;
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
                Transform child = transform.Find($"Slot{i + 1}").GetChild(0);
                child.GetComponent<Image>().sprite = inventory[i].image;
                child.GetComponent<Image>().enabled = true;
                break;
            }
        }
    }

    public bool isInventoryFull()
    {
        return numItemsInInventory == numSlotsFilled;
    }
}
