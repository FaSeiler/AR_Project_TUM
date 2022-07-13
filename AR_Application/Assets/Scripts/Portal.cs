using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
public class Portal : MonoBehaviour
{
    public static bool realWorld = true;
    GameObject underworld;
    AudioSource audioData;

    private GameObject cam;
    // Start is called before the first frame update
    void Start()
    {
        audioData = GetComponent<AudioSource>();
        // underworld = GameObject.Find("underworld");
        var objects = Resources.FindObjectsOfTypeAll<GameObject>();
        foreach (var obj in objects)
        {
            if (obj.name == "underworld")
            {
                underworld = obj;
                
                // obj.SetActive(!obj.activeSelf);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        // UIDebugText.ResetLog();
        // Debug.Log("Trigger!!!!!!!!!!!!!!!");
        UIDebugText.WriteLog("Trigggerrr!!!");
        UIDebugText.AddLog(other.gameObject.name);
        // print("hello");
        // AudioSource.PlayClipAtPoint(audioData.clip, transform.position);
        // Portal.realWorld = !Portal.realWorld;
        // other.gameObject.GetComponent<ARCameraBackground>().enabled = Portal.realWorld;
        // underworld.SetActive(!Portal.realWorld);
        // void turnOffSineWave() {
        //     cam.GetComponent<SineWave>().enabled = false;
        //     UIDebugText.WriteLog(cam.name);
        // }

        if(other.gameObject.name.Equals("AR Camera") || other.gameObject.tag.Equals("MainCamera")) 
        {
            Portal.realWorld = !Portal.realWorld;
            if(underworld)
            {
                underworld.SetActive(!Portal.realWorld);
            }
            // audioData.Play(0);
            AudioSource.PlayClipAtPoint(audioData.clip, transform.position);
            cam = other.gameObject;
            cam.GetComponent<ARCameraBackground>().enabled = Portal.realWorld;
            // cam.GetComponent<SineWave>().enabled = true;
            // Invoke("turnOffSineWave", 4);

        }

        
    }
    
}
