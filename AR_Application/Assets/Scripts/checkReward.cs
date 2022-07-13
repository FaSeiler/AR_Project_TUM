using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkReward : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject reward;
    // public GameObject orc;
    // public GameObject session;
    private GameObject goldCoins;
    void Start()
    {
        
    }

    // Update is called once per frame

    
    void Update()
    {
        // GameObject gc = null;
        if(Globals.goldShown && !Portal.realWorld) {
            // var objects = Resources.FindObjectsOfTypeAll<GameObject>();
            // foreach (var obj in objects)
            // {
            //     if (obj.name == "GoldCoins")
            //     {
            //         gc = obj;
            //     }
            // }

            var objects = Resources.FindObjectsOfTypeAll<GameObject>();
            foreach (var obj in objects)
            {
                // if (obj.name == "GoldCoins")
                // {
                //     goldCoins = obj;
                    
                //     // obj.SetActive(!obj.activeSelf);
                // }
                if (obj.name == "monster_orc_idle") {
                    obj.GetComponent<AudioSource>().Stop();
                }
            }



            GameObject player = GameObject.FindGameObjectsWithTag("MainCamera")[0];
            Vector3 playerPos = player.transform.position;
            Vector3 playerDirection = player.transform.forward;
            Quaternion playerRotation = player.transform.rotation;
            float spawnDistance = 0.5f;
            
            Vector3 spawnPos = playerPos + playerDirection*spawnDistance;
            GameObject go = new GameObject();
            go.transform.position = spawnPos;
            //rotation = spawnRotation;
            
            reward.GetComponent<Reward>().ShowReward(go.transform);
            this.gameObject.GetComponent<ImageTrackingPrefabs>().StopTracking();
            goldCoins = GameObject.FindGameObjectWithTag("Gold");
            goldCoins.SetActive(false);
            Globals.goldShown = false;
            
        }
    }
}
