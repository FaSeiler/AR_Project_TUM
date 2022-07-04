using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UIDebugText.WriteLog("hello");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void testing() {
        UIDebugText.WriteLog("testingggg button!!");
    }
}
