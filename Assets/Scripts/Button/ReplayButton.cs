using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayButton : MonoBehaviour
{    
    public void RestartGame()
    {
        SceneManagerer.instance.Next();
        Debug.Log("A");
    }

    public void Test()
    {
        Debug.Log("Button works");
    }
}
