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

    private int maxBattery;

    public static event Action OnPlayerDied;

    public int numSecondsFromMax;
    private float decreasePerSec;

    private bool batteryOut;
    
    // Start is called before the first frame update
    void Start()
    {
        ResetBattery();
    }

    // function to reset battery back to full
    void ResetBattery()
    {
        maxBattery = Initializer.maxBattery;
        Initializer.batteryPower = maxBattery;
        batteryOut = false;
        // if we know the duration of the battery we want from the max,we can calculate this as so
        decreasePerSec = (float) maxBattery / numSecondsFromMax;
    }

    // Update is called once per frame
    void Update()
    {
        //nothing left to do if the battery's out
        if(batteryOut)
        {
            return;
        }
        //decrease battery
        if (Initializer.worldFrozen == false) {
            Initializer.batteryPower -= decreasePerSec * Time.deltaTime;
        }

        UpdateBattery();
    }

    //update battery UI
    void UpdateBattery()
    {
        //if we ran out of battery, player is dead
        if(Initializer.batteryPower < 0)
        {
            Initializer.batteryPower = 0;
            batteryOut = true;
            OnPlayerDied.Invoke();
        }
        // change UI accordingly
        batterySlider.transform.localScale = new Vector3(Initializer.batteryPower / maxBattery, batterySlider.transform.localScale.y, batterySlider.transform.localScale.z);
        batteryText.text = $"{Mathf.CeilToInt(Initializer.batteryPower / maxBattery * 100)}%";
    }

    public void RestoreBattery(float restoreAmount)
    {
        Initializer.batteryPower += restoreAmount;
        if(Initializer.batteryPower > maxBattery)
        {
            Initializer.batteryPower = maxBattery;
        }
    }
}
