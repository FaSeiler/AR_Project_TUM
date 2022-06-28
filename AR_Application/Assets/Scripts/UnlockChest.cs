using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

/// <summary>
/// This class is responsible for checking if both the key and the chest are being tracked
/// and unlock the chest if thats true.
/// </summary>
[RequireComponent(typeof(ImageTrackingPrefabs))]
public class UnlockChest : MonoBehaviour
{
    private ImageTrackingPrefabs imageTrackingPrefabs;
    private ARTrackedImageManager trackedImageManager;
    private Animator chestAnimator;

    public static bool chestIsUnlocked = false;

    void Awake()
    {
        imageTrackingPrefabs = GetComponent<ImageTrackingPrefabs>();
        trackedImageManager = FindObjectOfType<ARTrackedImageManager>();
    }

    private void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += CheckForKeyChestPair;
    }

    private void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= CheckForKeyChestPair;
    }

    private void CheckForKeyChestPair(ARTrackedImagesChangedEventArgs eventArgs)
    {

        GameObject chestGO = imageTrackingPrefabs.spawnedPrefabs["chest"];
        GameObject keyGO = imageTrackingPrefabs.spawnedPrefabs["key"];

        if (imageTrackingPrefabs.activePrefabs.Contains(chestGO) && imageTrackingPrefabs.activePrefabs.Contains(keyGO))
        {
            StartCoroutine(Unlock(keyGO, chestGO));
        }

    }
    IEnumerator Unlock(GameObject keyGO, GameObject chestGO)
    {
        UIDebugText.AddLog("Chest & Key ACTIVE");

        yield return new WaitForSeconds(1.5f);

        keyGO.SetActive(false);

        Chest chest = chestGO.GetComponentInChildren<Chest>();
        chest.PlayUnlockAnimation();

        chestIsUnlocked = true;
    }

    public void DisableChestAndKey(GameObject chest, GameObject key)
    {
        chest.SetActive(false);
        key.SetActive(false);
    }
}
