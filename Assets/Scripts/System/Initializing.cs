using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializing : MonoBehaviour {
    [SerializeField] RenderTexture RTINIT;
    [SerializeField] GameObject PixelatedPanelINIT;
    [SerializeField] Camera PixelCameraINIT;

    void Start() {
        Initializer.RT = RTINIT;
        Initializer.PixelatedPanel = PixelatedPanelINIT;
        Initializer.PixelCamera = PixelCameraINIT;
    }
}
