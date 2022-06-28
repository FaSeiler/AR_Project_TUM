using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

/// <summary>
/// This class lets you display prefabs on corresponding markers.
/// The name of the prefab given in 'placeablePrefabs' needs to be the same as the marker's name in the XRreferenceLibrary.
/// </summary>
[RequireComponent(typeof(ARTrackedImageManager))]
public class ImageTrackingPrefabs : MonoBehaviour
{
    [SerializeField]
    private GameObject[] placeablePrefabs;

    [HideInInspector]
    public ARTrackedImageManager trackedImageManager;

    public Dictionary<string, GameObject> spawnedPrefabs = new Dictionary<string, GameObject>();
    
    // All prefabs that are currently active in the scene. Only prefabs where the corresponding marker is being tracked are active.
    public List<GameObject> activePrefabs = new List<GameObject>();

    private void Awake()
    {
        trackedImageManager = FindObjectOfType<ARTrackedImageManager>();

        foreach (GameObject prefab in placeablePrefabs)
        {
            GameObject newPrefab = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            newPrefab.name = prefab.name;
            spawnedPrefabs.Add(prefab.name, newPrefab);
            newPrefab.SetActive(false);
        }
    }

    private void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += ImageChanged;
    }

    private void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= ImageChanged;
    }

    private void ImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            UpdateImage(trackedImage);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            UpdateImage(trackedImage);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            spawnedPrefabs[trackedImage.name].SetActive(false);
        }

        UpdateActivePrefabs();

    }

    public void StopTracking()
    {
        trackedImageManager.trackedImagesChanged -= ImageChanged;
        trackedImageManager.enabled = false;
        UIDebugText.AddLog("Tracking Stopped!");
    }

    public void StartTracking()
    {
        trackedImageManager.trackedImagesChanged += ImageChanged;
        trackedImageManager.enabled = true;
        UIDebugText.AddLog("Tracking Started!");
    }

    private void UpdateImage(ARTrackedImage trackedImage)
    {
        string name = trackedImage.referenceImage.name;
        Vector3 position = trackedImage.transform.position;

        GameObject prefab = spawnedPrefabs[name];
        prefab.transform.position = position;

        if (trackedImage.trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Limited
            || trackedImage.trackingState == UnityEngine.XR.ARSubsystems.TrackingState.None)
        {
            prefab.SetActive(false);
        }
        else
        {
            prefab.SetActive(true);
        }
    }

    /// <summary>
    /// Update Debug Log with all markers that are currently being tracked.
    /// </summary>
    private void UpdateActivePrefabs()
    {
        activePrefabs.Clear();
        UIDebugText.ResetLog();

        foreach (GameObject go in spawnedPrefabs.Values)
        {
            if (go.activeSelf)
            {
                activePrefabs.Add(go);
                UIDebugText.AddLog("Active: " + go.name);
            }
        }
    }

    public void DisableTracking()
    {
        trackedImageManager.enabled = false;
    }
}

