using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalFalling : MonoBehaviour
{
    public Material tmpRed;
    public Material tmpGreen;
    public MeshRenderer meshRenderer;
    public MeshRenderer meshRenderer2;

    public int nrCollidedObjects = 0;

    public Transform bucketTransform;
    private FallingObjectController fallingObjectController;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Enter");

        if (other.tag == "FallingObject")
        {
            meshRenderer.material = tmpRed;
            Debug.Log("FallingObject Collision");
            other.gameObject.GetComponent<FallingObject>().DisableThisGO();
            fallingObjectController.AddToScore();

            StartCoroutine(ChangeColor());
        }
    }

    private IEnumerator ChangeColor()
    {
        meshRenderer.material = tmpGreen;

        yield return new WaitForSeconds(1.0f);

        meshRenderer.material = tmpRed;
    }

    private void OnEnable()
    {
        fallingObjectController = FindObjectOfType<FallingObjectController>();
        fallingObjectController.StartGame();

        meshRenderer.material = tmpRed;

        nrCollidedObjects = 0;
    }

    private void OnDisable()
    {
        nrCollidedObjects = 0;
        StopAllCoroutines();
        fallingObjectController.StopGame();
    }

    public void DisableMesh()
    {
        meshRenderer.enabled = false;
        meshRenderer2.enabled = false;
    }
}
