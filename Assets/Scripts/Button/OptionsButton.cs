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

    public void SetMusic(System.Single sliderValue)
    {
        SceneManagerer.instance.volumeSet(sliderValue);
    }

    public void QuitGame()
    {
        SceneManagerer.instance.Quit();
        Debug.Log("Ok so this runs");
    }

    public void ResumeGame()
    {
        SceneManagerer.instance.removePause();
        Debug.Log("Ok so this runs");
    }

    public void RestartGame()
    {
        SceneManagerer.instance.Next();
        Debug.Log("Ok so this runs");
    }

    public void SetSFX(System.Single sliderValue)
    {
        SceneManagerer.instance.SFXvolumeSet(sliderValue);
    }

    public void GoToTitle()
    {
        SceneManagerer.instance.Title();
        Debug.Log("Ok so this runs");
    }


}
