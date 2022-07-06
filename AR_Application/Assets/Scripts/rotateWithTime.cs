using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateWithTime : MonoBehaviour
{


    // Start is called before the first frame update
    public float RotationSpeed;
    public Vector3 rotationVector = Vector3.up;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate ( rotationVector * ( RotationSpeed * Time.deltaTime ) );
    }
}
