using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkReward : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject reward;
    public GameObject orc;
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
            reward.GetComponent<Reward>().ShowReward(orc.transform);
            
        }
    }
}
