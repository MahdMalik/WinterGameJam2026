using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField]
    protected string itemName;
    [SerializeField]
    protected Vector2 originalWorldPos;
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
        transform.position = new Vector3(originalWorldPos[0], originalWorldPos[1], 3.8f);
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
        if(!theItemUi.IsInventoryFull())
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

    public void DropItem(Vector2 inputPos)
    {
        transform.position = new Vector3(inputPos[0], inputPos[1], 3.8f);

        inInventory = false;
        spriteRenderer.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name != "Player" || inInventory)
        {
            Debug.Log($"Fail, actual name was {other.gameObject.name}");
            return;
        }

        Debug.Log("Player picked up item");

        ItemTouched();
    }

}
