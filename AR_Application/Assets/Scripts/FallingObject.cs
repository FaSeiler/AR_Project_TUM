using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : MonoBehaviour
{
    public float fallSpeed = 0.1f;
    public float spinSpeed = 10f;

    public GameObject explosionEffectGO;
    public GameObject bucketGO;

    private void Start()
    {
        float rand = Random.Range(0, 1);
        if (rand == 0)
        {
            spinSpeed = -spinSpeed;
        }
    }

    void Update()
    {
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime, Space.World);
        transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);

        if (transform.position.y < bucketGO.transform.position.y)
        {
            GameObject go = Instantiate(explosionEffectGO, this.gameObject.transform.position, this.gameObject.transform.rotation);
            go.GetComponent<ExplosionEffect>().PlayExplosion();
            DisableThisGO();
        }
    }

    public void DisableThisGO()
    {
        this.gameObject.SetActive(false);
    }
}
