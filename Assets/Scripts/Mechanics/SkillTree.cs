using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkillTree : MonoBehaviour
{
    public TMP_Text pointsLastRound;
    public TMP_Text currentPoints;
    
    // Start is called before the first frame update
    void Start()
    {
        pointsLastRound.text = $"POINTS OBTAINED LAST ROUND: {Initializer.pointsLastRun}";
        currentPoints.text = $"CURRENT POINTS: {Initializer.perkPoints}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
