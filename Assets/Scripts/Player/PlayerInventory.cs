using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private int chosenItemIndex;

    public ItemUI theItemUi;
    
    // Start is called before the first frame update
    void Start()
    {
        chosenItemIndex = 0;
        theItemUi.SetItem(chosenItemIndex);
    }

    void SwapSelectedSlot(int newIndex)
    {
        chosenItemIndex = theItemUi.SetItem(newIndex);
    }

    // Update is called once per frame
    void Update()
    {
        float scroll = Input.mouseScrollDelta.y;
        if(Input.GetKeyDown(KeyCode.E) || scroll > 0)
        {
            SwapSelectedSlot(chosenItemIndex + 1);
        }
        if(Input.GetKeyDown(KeyCode.Q) || scroll < 0)
        {
            SwapSelectedSlot(chosenItemIndex - 1);
        }
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwapSelectedSlot(0);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwapSelectedSlot(1);
        }
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwapSelectedSlot(2);
        }
        if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            SwapSelectedSlot(3);
        }

        if(Input.GetKeyDown(KeyCode.T))
        {
            
        }
    }
}
