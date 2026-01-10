using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Battery : MonoBehaviour
{
    public Image batterySlider;
    public TMP_Text batteryText;

    public int maxBattery;

    public static event Action OnPlayerDied;

    public int numSecondsFromMax;

    private float currentBattery;

    private float decreasePerSec;

    private bool batteryOut;
    
    // Start is called before the first frame update
    void Start()
    {
        PlayerMovement.ResetGame += ResetBattery;
        ResetBattery();
    }

    void ResetBattery()
    {
        currentBattery = maxBattery;
        batteryOut = false;
        decreasePerSec = (float) maxBattery / numSecondsFromMax;
    }

    // Update is called once per frame
    void Update()
    {
        if(batteryOut)
        {
            return;
        }
        currentBattery -= decreasePerSec * Time.deltaTime;

        UpdateBattery();
    }

    void UpdateBattery()
    {
        if(currentBattery < 0)
        {
            currentBattery = 0;
            batteryOut = true;
            OnPlayerDied.Invoke();
        }
        batterySlider.transform.localScale = new Vector3(currentBattery / maxBattery, batterySlider.transform.localScale.y, batterySlider.transform.localScale.z);
        batteryText.text = $"{Mathf.CeilToInt(currentBattery / maxBattery * 100)}%";
    }
}
