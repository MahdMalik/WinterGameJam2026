    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    protected string name;
    protected Vector2 worldPos;
    protected bool inInventory = false;

    private Texture2D image;

    private float cooldown;


    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void Activate()
    {
        
    }
}
