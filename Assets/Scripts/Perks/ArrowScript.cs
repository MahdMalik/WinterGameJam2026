using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(!Initializer.guidingArrow)
        {
            gameObject.SetActive(false);
        }
    }
}
