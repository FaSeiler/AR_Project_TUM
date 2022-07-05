using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerLoader : MonoBehaviour
{
    public GameObject spawnerPrefab;

    private GameObject spawnerGO; // The spawned Spawner

    private void OnEnable()
    {
        spawnerGO = Instantiate(spawnerPrefab, transform.position, transform.rotation);
        spawnerGO.GetComponent<ObjectSpawner>().bucketGO = this.gameObject;
        spawnerGO.GetComponent<ObjectSpawner>().SetSpawnAreaPosition(this.gameObject.transform);
    }

    private void OnDisable()
    {
        Destroy(spawnerGO);
    }
}
