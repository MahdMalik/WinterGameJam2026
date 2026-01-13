using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXObject : MonoBehaviour
{
    [SerializeField] private AudioSource SFXSource;
    void Update()
    {
        SFXSource.volume = Initializer.SFXVolume;
    }
}
