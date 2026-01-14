using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public PlayerMovement player;
    
    [SerializeField]
    protected string itemName;
    [SerializeField]
    protected Vector2 originalWorldPos;
    [SerializeField]
    protected bool inInventory = false;

    public Sprite image;

    [SerializeField]
    protected float cooldown;

    protected bool inCooldown;
    
    [SerializeField]
    protected float activationTime;
    protected bool activated;

    protected SpriteRenderer spriteRenderer;
    public InventoryManager theInventoryManager;
    protected float startTime;

    [SerializeField]
    protected bool oneTimeActivation;

    protected bool usedUp;
    
    // Start is called before the first frame update
    protected virtual void Start()
    {
        // want to make sure when the item is removed, we call its function FOR ALL items
        InventoryManager.itemsRemoved += ResetItem;
        ResetItem();
    }

    // function to reset the item to its original position out of the user's hands
    public void ResetItem()
    {
        //Debug.Log("This should be running");
        transform.position = new Vector3(originalWorldPos[0], originalWorldPos[1], 3.8f);
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = true;
        spriteRenderer.sprite = image;
        inCooldown = false;
        activated = false;
        usedUp = false;

        // for lightsaber, its already in your inventory, so we do this accordingly
        if(name == "Lightsaber")
        {
            ItemTouched();
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(!oneTimeActivation)
        {
            // if we've activated it and its activation ran out, go into cooldown mode. We say that player can't turn
            // until its down for cases like a sword slash not turning with the player
            if(activated && Time.time - startTime > activationTime)
            {
                inCooldown = true;
                activated = false;
                startTime = Time.time;
                PlayerVars.canTurnInteract = true;
            }
            // when the cooldown is over, we say the item can be used again
            else if (inCooldown && Time.time - startTime > cooldown)
            {
                inCooldown = false;
            }
        }
    }

    // kinda resets the item back to its initial place
    void DestroyItem()
    {
        inInventory = false;
        transform.position = new Vector3(originalWorldPos[0], originalWorldPos[1], 3.8f);
        usedUp = true;
    }

    // this activates the item in question
    public virtual void Activate()
    {
        if(oneTimeActivation)
        {
            DestroyItem();
            theInventoryManager.RemoveItem();
        }
        else
        {
            activated = true;
            // starts timer too
            startTime = Time.time;
            PlayerVars.canTurnInteract = false;
        }
    }


    // rutn this when the item is touched
    protected void ItemTouched()
    {
        // make sure the inventory aint' full already
        if(!theInventoryManager.IsInventoryFull())
        {
            // add it to inventory as so
            
            // Debug.Log("GRAHH");
            inInventory = true;
            spriteRenderer.enabled = false;
            theInventoryManager.AddItem(this);
        }
        else
        {
            // Debug.Log("no no");
        }
    }

    // drops the item a bit in front of the player
    public void DropItem(Vector2 inputPos)
    {
        // sets position to input
        transform.position = new Vector3(inputPos[0], inputPos[1], 3.8f);

        //actually removes it from inventory
        inInventory = false;
        spriteRenderer.enabled = true;
    }

    // when the item is touched by the player
    private void OnTriggerEnter2D(Collider2D other)
    {
        // make sure the player touched it and that it isn't already in their inventory
        if (other.gameObject.name != "Player" || inInventory || usedUp)
        {
            Debug.Log($"Fail, actual name was {other.gameObject.name}");
            return;
        }

        Debug.Log("Player picked up item");

        // call the function to add it to inventory
        ItemTouched();
    }

    //getter functions
    public bool getCooldown()
    {
        return inCooldown;
    }

    public bool getActivated()
    {
        return activated;
    }

    void OnDestroy()
    {
        InventoryManager.itemsRemoved -= ResetItem;
    }
}
