using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : MonoBehaviour
{
    public float fallSpeed = 0.1f;
    public float spinSpeed = 0.0f;

    void Update()
    {
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime, Space.World);
        transform.Rotate(Vector3.forward, spinSpeed * Time.deltaTime);

        if (transform.position.y < -2.0f)
        {
            DisableThisGO();
        }
    }

    public void DisableThisGO()
    {
        this.gameObject.SetActive(false);
    }
}
