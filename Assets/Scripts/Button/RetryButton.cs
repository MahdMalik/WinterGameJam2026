using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetryButton : MonoBehaviour
{    
    void Awake()
    {
        Debug.Log("HELP IN GAIA");
    }
    
    public void RestartGame()
    {
        SceneManagerer.instance.Next();
        Debug.Log("Ok so this runs");
    }
}
