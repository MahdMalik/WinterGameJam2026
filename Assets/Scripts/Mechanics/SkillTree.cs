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

    void AdjustBoughtText(Transform mainPerkObject, bool boughtStatus)
    {
        mainPerkObject.GetChild(0).Find("BuyPerk").GetChild(0).GetComponent<TextMeshProUGUI>().text = boughtStatus ? "Bought" : "Buy";
    }
    
    // Start is called before the first frame update
    void Start()
    {
        perkMenuOpen = false;

        pointsLastRound.text = $"POINTS OBTAINED LAST ROUND: {Initializer.pointsLastRun}";
        currentPoints.text = $"CURRENT POINTS: {Initializer.perkPoints}";

        foreach(Perk thePerk in Initializer.perks)
        {
            Transform childObject = transform.Find("Perks").Find($"{thePerk.perkName}{(thePerk.upgradedVer ? "Upgraded" : "")}");
            childObject.localPosition =  thePerk.perkUiPos;
            // Debug.Log($"Uhhh it was {thePerk.perkUiPos}");
            childObject.GetChild(0).gameObject.SetActive(false);

            childObject.GetChild(0).Find("PerkName").GetComponent<TextMeshProUGUI>().text = thePerk.perkName;
            childObject.GetChild(0).Find("PerkDesc").GetComponent<TextMeshProUGUI>().text = thePerk.perkDesc;
            childObject.GetChild(0).Find("PerkCost").GetComponent<TextMeshProUGUI>().text = $"Cost: {thePerk.perkCost} Points";
            AdjustBoughtText(childObject, thePerk.activated);
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
                    AdjustBoughtText(buttonPressed.transform.parent.parent, true);
                }
                break;
            }
        }
    }
}
