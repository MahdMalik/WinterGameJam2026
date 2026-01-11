using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField]
    protected string itemName;
    [SerializeField]
    protected Vector2 worldPos;
    [SerializeField]
    protected bool inInventory = false;

    public Sprite image;

    [SerializeField]
    private float cooldown;

    private SpriteRenderer spriteRenderer;

    public static event Action<Item> PlayerGotItem;

    public ItemUI theItemUi;
    
    // Start is called before the first frame update
    protected virtual void Start()
    {
        // Debug.Log("So this parent runs");
        transform.position = new Vector3(worldPos[0], worldPos[1], 3.8f);
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = image;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    protected virtual void Activate()
    {
        
    }

    protected void ItemTouched()
    {
        if(!theItemUi.isInventoryFull())
        {
            // Debug.Log("GRAHH");
            inInventory = true;
            spriteRenderer.enabled = false;
            PlayerGotItem.Invoke(this);
        }
        else
        {
            // Debug.Log("no no");
        }
    }
}
