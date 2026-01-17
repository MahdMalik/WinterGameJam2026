using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeButton : MonoBehaviour
{
    void Awake()
    {
        Debug.Log("HELP IN GAIA");
    }
    
    public void ResumeGame()
    {
        SceneManagerer.instance.Unpause();
        Debug.Log("Ok so this runs");
    }
}
