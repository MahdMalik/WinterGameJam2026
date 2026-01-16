using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public InventoryManager theInventoryManager;
    
    // Start is called before the first frame update
    void Start()
    {
        // say for now we select the first slot (at index 0)
        theInventoryManager.SetSelectedItem(0);
    }

    // Update is called once per frame
    void Update()
    {
        float scroll = Input.mouseScrollDelta.y;
        // buttons to select different inventory items
        if(Input.GetKeyDown(KeyCode.E) || scroll > 0)
        {
            theInventoryManager.IncrementSelectedItem();
        }
        if(Input.GetKeyDown(KeyCode.Q) || scroll < 0)
        {
            theInventoryManager.DecrementSelectedItem();
        }
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            // Thsi is 1 less than the actual number we press due to indexing starting at 0
            theInventoryManager.SetSelectedItem(0);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            theInventoryManager.SetSelectedItem(1);
        }
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            theInventoryManager.SetSelectedItem(2);
        }
        if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            theInventoryManager.SetSelectedItem(3);
        }

        // when the player is throwing their item away
        if(Input.GetKeyDown(KeyCode.T))
        {
            // remove it from inventory
            Item droppedItem = theInventoryManager.RemoveItem();

            // if there was actually something to remove, get its position relative to the player and put it there
            if(droppedItem != null)
            {

                droppedItem.DropItem(HelperFunctions.PutDistanceAway(transform.position, 1.5f));
            }

        }

        // activates the item's ability
        if(Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.F))
        {
            theInventoryManager.ActivateItem();
        }
    }
}
