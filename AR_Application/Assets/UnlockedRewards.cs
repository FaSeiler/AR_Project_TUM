using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnlockedRewards : MonoBehaviour
{
    public PlayerPrefsManager prefsManager;
    public TextMeshProUGUI nrRewardsText;

    void Start()
    {
        string outputText = "Crowns collected: " + prefsManager.GetNrOfUnlockedLevels().ToString();
        nrRewardsText.text = outputText;
    }


}
