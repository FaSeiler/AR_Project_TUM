using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalSineWave : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void turnOffSineWave() {
        this.gameObject.GetComponent<SineWave>().enabled = false;
    }
    void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Portal") {
            this.gameObject.GetComponent<SineWave>().enabled = true;
            Invoke("turnOffSineWave", 3.0f);
        }
    }
}
