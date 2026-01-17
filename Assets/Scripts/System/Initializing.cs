using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializing : MonoBehaviour {
    [SerializeField] RenderTexture RTINIT;
    [SerializeField] GameObject PixelatedPanelINIT;
    [SerializeField] Camera PixelCameraINIT;

    void Awake() {
        Initializer.RT = RTINIT;
        Initializer.PixelatedPanel = PixelatedPanelINIT;
        Initializer.PixelCamera = PixelCameraINIT;
    }
    
    //Initializes some global variables.
    public void Initialization() {
        Initializer.RT = RTINIT;
        Initializer.PixelatedPanel = PixelatedPanelINIT;
        Initializer.PixelCamera = PixelCameraINIT;
        // Debug.Log(Initializer.PixelCamera);
    }
    
}
