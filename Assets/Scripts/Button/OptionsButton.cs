using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsButton : MonoBehaviour
{
    void Awake()
    {
        Debug.Log("HELP IN GAIA");
    }
    
    public void OpenOptions()
    {        
        SceneManagerer.instance.VolumeBars();
        Debug.Log("Ok so this runs");
    }
}
