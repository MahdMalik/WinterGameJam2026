using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButton : MonoBehaviour
{
    void Awake()
    {
        Debug.Log("HELP IN GAIA");
    }
    
    public void QuitGame()
    {
        SceneManagerer.instance.Quit();
        Debug.Log("Ok so this runs");
    }
}
