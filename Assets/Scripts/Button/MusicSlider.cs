using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSlider : MonoBehaviour
{
    public void SetMusic(System.Single sliderValue)
    {
        SceneManagerer.instance.volumeSet(sliderValue);
    }
}
