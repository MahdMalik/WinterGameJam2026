using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXSlider : MonoBehaviour
{
    public void SetSFX(System.Single sliderValue)
    {
        SceneManagerer.instance.SFXvolumeSet(sliderValue);
    }
}
