using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(!Portal.realWorld) {
            UIDebugText.WriteLog("Gold in underworld!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
