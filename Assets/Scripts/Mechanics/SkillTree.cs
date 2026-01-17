using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillTree : MonoBehaviour
{
    public TMP_Text pointsLastRound;
    public TMP_Text currentPoints;

    private bool perkMenuOpen;

    void UpdateLastPoints()
    {
        pointsLastRound.text = $"POINTS OBTAINED LAST ROUND: {Initializer.pointsLastRun}";
    }

    void UpdateCurrentPoints()
    {
        currentPoints.text = $"CURRENT POINTS: {Initializer.perkPoints}";
    }

    void AdjustBoughtText(Transform mainPerkObject, Perk thePerk)
    {
        mainPerkObject.GetChild(0).Find("BuyPerk").GetChild(0).GetComponent<TextMeshProUGUI>().text = thePerk.activated ? "Bought" : thePerk.CheckAvailableStatus() ? "Buy" : "Locked";
    }
    
    // Start is called before the first frame update
    void Start()
    {
        perkMenuOpen = false;

        UpdateLastPoints();
        UpdateCurrentPoints();

        foreach(Perk thePerk in Initializer.perks)
        {
            Transform childObject = transform.Find("Perks").Find($"{thePerk.perkName}{(thePerk.upgradedVer ? "Upgraded" : "")}");
            childObject.localPosition =  thePerk.perkUiPos;
            // Debug.Log($"Uhhh it was {thePerk.perkUiPos}");
            childObject.GetChild(0).gameObject.SetActive(false);

            childObject.GetChild(0).Find("PerkName").GetComponent<TextMeshProUGUI>().text = thePerk.perkName;
            childObject.GetChild(0).Find("PerkDesc").GetComponent<TextMeshProUGUI>().text = thePerk.perkDesc;
            childObject.GetChild(0).Find("PerkCost").GetComponent<TextMeshProUGUI>().text = $"Cost: {thePerk.perkCost} Points";
            AdjustBoughtText(childObject, thePerk);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PerkDataOpened()
    {
        GameObject buttonPressed = EventSystem.current.currentSelectedGameObject;
        if(!perkMenuOpen) buttonPressed.transform.GetChild(0).gameObject.SetActive(true);
        perkMenuOpen = true;
    }

    public void PerkDataClosed(GameObject theButton)
    {
        theButton.transform.parent.gameObject.SetActive(false);
        perkMenuOpen = false;
    }

    public void PerkDataClosed()
    {
        PerkDataClosed(EventSystem.current.currentSelectedGameObject);
    }

    public void PurchasedItem()
    {
        GameObject buttonPressed = EventSystem.current.currentSelectedGameObject;
        string perkName = buttonPressed.transform.parent.parent.name;
        foreach(Perk thePerk in Initializer.perks)
        {
            if(thePerk.perkName == perkName)
            {
                if(thePerk.PerkPurchase())
                {
                    PerkDataClosed(buttonPressed);
                    AdjustBoughtText(buttonPressed.transform.parent.parent, thePerk);
                    UpdateCurrentPoints();
                }
            }
            //if we just bought the item, want to update the next item. we are guarantted to always come across it in the array AFTER the original too.
            else if(thePerk.prevPerk != null && thePerk.prevPerk.perkName == perkName && thePerk.prevPerk.activated)
            {
                Debug.Log("This should run!");
                AdjustBoughtText(buttonPressed.transform.parent.parent.parent.Find($"{thePerk.perkName}{(thePerk.upgradedVer ? "Upgraded" : "")}"), thePerk);
                break;
            }
        }
    }
}
