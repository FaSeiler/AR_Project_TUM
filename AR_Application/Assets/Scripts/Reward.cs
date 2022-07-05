using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.SceneManagement;


public class Reward : MonoBehaviour
{
    public int rewardId = 0;

    [SerializeField]
    private PlayerPrefsManager playerPrefsManager;

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

    public void CollectReward()
    {
        playerPrefsManager.UnlockLevelById(rewardId);
        SceneManager.LoadScene(0);

    }
}
