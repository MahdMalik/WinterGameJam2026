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
        //make sure that when the game is restarted, we obviously reset battery
        PlayerMovement.ResetGame += ResetBattery;
        ResetBattery();
    }

    // function to reset battery back to full
    void ResetBattery()
    {
        currentBattery = maxBattery;
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
        currentBattery -= decreasePerSec * Time.deltaTime;

        UpdateBattery();
    }

    //update battery UI
    void UpdateBattery()
    {
        //if we ran out of battery, player is dead
        if(currentBattery < 0)
        {
            currentBattery = 0;
            batteryOut = true;
            OnPlayerDied.Invoke();
        }
        // change UI accordingly
        batterySlider.transform.localScale = new Vector3(currentBattery / maxBattery, batterySlider.transform.localScale.y, batterySlider.transform.localScale.z);
        batteryText.text = $"{Mathf.CeilToInt(currentBattery / maxBattery * 100)}%";
    }
}
