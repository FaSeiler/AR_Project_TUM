using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

/// <summary>
/// This loads the correct ReferenceImageLibrary for the current scene.
/// </summary>
public class TrackingLibraryLoader : MonoBehaviour
{

    [SerializeField]
    private XRReferenceImageLibrary[] referenceLibraries = null;
    private ARTrackedImageManager trackedImageManager = null;
    
    public int libIndexToUse = 0;

    private void Start()
    {
        trackedImageManager = GetComponent<ARTrackedImageManager>();
        SwitchImageLibrary();
    }

    void SwitchImageLibrary()
    {
        Debug.Log("Setting reference library with index " + libIndexToUse);
        trackedImageManager.enabled = false;

        // Creating a runtime library will throw an exception when not run on a phone.
        try
        {
            trackedImageManager.referenceLibrary = trackedImageManager.CreateRuntimeLibrary(referenceLibraries[libIndexToUse]);
        }
        catch (System.Exception)
        {
            Debug.LogWarning("No image tracking subsystem found.");
        }

        trackedImageManager.enabled = true;
    }

    /*private void Update()
    {
        UIDebugText.ResetLog();
        UIDebugText.AddLog("Name:" + trackedImageManager.name);
        UIDebugText.AddLog("Enabled: " + trackedImageManager.enabled.ToString());
        UIDebugText.AddLog("ActiveLib: " + libIndexToUse);
        if (trackedImageManager.referenceLibrary != null)
        {
            UIDebugText.AddLog("Count: " + trackedImageManager.referenceLibrary.count.ToString());
            UIDebugText.AddLog("ReferenceLibrary: " + trackedImageManager.referenceLibrary.ToString());
        }
        
    }*/
}
