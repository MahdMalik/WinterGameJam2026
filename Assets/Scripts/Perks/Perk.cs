using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Perk
{
    public string perkNameGameObj;
    public string perkDesc;
    public int perkCost;

    public bool activated;
    public Perk prevPerk;
    public Vector2 perkUiPos;

    public string perkNameDisplayed;

    public Perk(string name, string desc, int cost, Perk prev, Vector2 pos, bool ifUpgrade, string displayedName)
    {
        perkNameGameObj = $"{name}{(ifUpgrade ? "Upgraded" : "")}";
        perkDesc = desc;
        perkCost = cost;
        activated = false;
        prevPerk = prev;
        perkUiPos = pos;
        perkNameDisplayed = displayedName + (ifUpgrade ? "  Upgraded" : "");
    }

    public bool CheckAvailableStatus()
    {
        return prevPerk == null || prevPerk.activated == true;
    }
    
    public bool PerkPurchase()
    {
        if(!activated && Initializer.perkPoints >= perkCost && CheckAvailableStatus() )
        {
            Debug.Log("Purchase yes!");
            activated = true;
            ActivatePerk();
            Initializer.perkPoints -= perkCost;
            return true;
        }
        else
        {
            Debug.Log("Purchase fail!");
            return false;
        }

    }

    protected abstract void ActivatePerk();
}
