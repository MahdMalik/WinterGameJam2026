using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public ItemUI theItemUi;
    
    // Start is called before the first frame update
    void Start()
    {
        theItemUi.SetSelectedItem(0);
    }

    // Update is called once per frame
    void Update()
    {
        float scroll = Input.mouseScrollDelta.y;
        if(Input.GetKeyDown(KeyCode.E) || scroll > 0)
        {
            theItemUi.IncrementSelectedItem();
        }
        if(Input.GetKeyDown(KeyCode.Q) || scroll < 0)
        {
            theItemUi.DecrementSelectedItem();
        }
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            theItemUi.SetSelectedItem(0);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            theItemUi.SetSelectedItem(1);
        }
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            theItemUi.SetSelectedItem(2);
        }
        if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            theItemUi.SetSelectedItem(3);
        }

        if(Input.GetKeyDown(KeyCode.T))
        {
            Item droppedItem = theItemUi.RemoveItem();

            if(droppedItem != null)
            {
                float xOffset = PlayerVars.PlayerFacing == 2 ? 1 : PlayerVars.PlayerFacing == 4 ? - 1 : 0; 
                float yOffset = PlayerVars.PlayerFacing == 1 ? 1 : PlayerVars.PlayerFacing == 3 ? -1 : 0;

                Vector2 thrownPosition = new Vector2(transform.position.x + xOffset, transform.position.y + yOffset);

                droppedItem.DropItem(thrownPosition);
            }

        }
    }
}
