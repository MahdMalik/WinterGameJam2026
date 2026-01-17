using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Perk
{
    public string perkName;
    public string perkDesc;
    public int perkCost;

    public bool activated;
    public Perk prevPerk;
    public Vector2 perkUiPos;

    public bool upgradedVer;

    public Perk(string name, string desc, int cost, Perk prev, Vector2 pos, bool ifUpgrade)
    {
        perkName = name;
        perkDesc = desc;
        perkCost = cost;
        activated = false;
        prevPerk = prev;
        perkUiPos = pos;
        upgradedVer = ifUpgrade;
    }
    
    public bool PerkPurchase()
    {
        if(!activated && Initializer.perkPoints >= perkCost && (prevPerk == null || prevPerk.activated == true) )
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
