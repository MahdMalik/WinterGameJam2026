using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightsaber : Item
{
    // Start is called before the first frame update
    protected override void Start()
    {
        // Debug.Log("So this cihld runs");
        base.Start();
        ItemTouched();
    }

    // Update is called once per frame
    protected override void Update()
    {
        
    }
}
