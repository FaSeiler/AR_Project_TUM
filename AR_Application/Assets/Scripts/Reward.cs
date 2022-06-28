using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;


public class Reward : MonoBehaviour
{
    [SerializeField]
    private GameObject rewardModelGO;

    [SerializeField]
    private Button collectButton;

    private void Start()
    {
        rewardModelGO.SetActive(false);
        collectButton.gameObject.SetActive(false);
    }

    public void ShowReward(Transform spawnLocation)
    {
        rewardModelGO.transform.position = spawnLocation.position;
        rewardModelGO.SetActive(true);

        collectButton.gameObject.SetActive(true);
    }
}
