using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalFalling : MonoBehaviour
{
    public Material tmpRed;
    public Material tmpGreen;
    public MeshRenderer meshRenderer;

    public int nrCollidedObjects = 0;

    public Transform bucketTransform;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Enter");

        if (other.tag == "FallingObject")
        {
            meshRenderer.material = tmpRed;
            Debug.Log("FallingObject Collision");
            other.gameObject.GetComponent<FallingObject>().DisableThisGO();
            nrCollidedObjects++;
            UIDebugText.AddLog("Collsion Count: " + nrCollidedObjects);

            StartCoroutine(ChangeColor());
        }
    }

    private IEnumerator ChangeColor()
    {
        meshRenderer.material = tmpGreen;

        yield return new WaitForSeconds(1.0f);

        meshRenderer.material = tmpRed;
    }

    //private void OnTriggerExit(Collider other)
    //{
    //Debug.Log("Exit");
    //}

    private void OnEnable()
    {
        nrCollidedObjects = 0;
    }

    private void OnDisable()
    {
        nrCollidedObjects = 0;
        StopAllCoroutines();
    }
}
