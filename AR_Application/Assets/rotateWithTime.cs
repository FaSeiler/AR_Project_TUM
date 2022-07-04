using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateWithTime : MonoBehaviour
{
    // Start is called before the first frame update
    public float RotationSpeed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate ( Vector3.up * ( RotationSpeed * Time.deltaTime ) );
    }
}
