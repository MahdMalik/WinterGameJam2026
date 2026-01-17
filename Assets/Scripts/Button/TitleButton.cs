using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleButton : MonoBehaviour
{
    void Awake()
    {
        Debug.Log("HELP IN GAIA");
    }
    
    public void GoToTitle()
    {
        SceneManagerer.instance.Title();
        Debug.Log("Ok so this runs");
    }
}
