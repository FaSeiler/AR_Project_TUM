using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject spawnablePrefab;
    private ARAnchor anchor;

    public float spawnRateSeconds = 2.0f;
    public float spawnArea = 1.0f;
    private const int amountToPool = 20;
    public float yAxisOffset = 1.2f;


    public GameObject bucketGO;

    private List<GameObject> spawnedPrefabsPool = new List<GameObject>();

    private void OnEnable()
    {
        StartSpawningObjects();
    }

    private void OnDisable()
    {
        StopSpawningObjects();
        ClearSpawnedObjectsPool();
        
        if (anchor != null) // Remove Anchor
        {
            anchor = null;
            Destroy(this.gameObject.GetComponent<ARAnchor>());
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SpawnObject();
        }
    }

    public void SetSpawnAreaPosition(Transform transform)
    {
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y + yAxisOffset, transform.position.z);
        this.gameObject.transform.position = newPosition;
        this.gameObject.transform.rotation = transform.rotation;

        anchor = this.gameObject.GetComponent<ARAnchor>();
        if (anchor == null)
        {
            anchor = this.gameObject.AddComponent<ARAnchor>();
        }
        else
        {
            UIDebugText.AddLog($"Created regular anchor (id: {anchor.nativePtr}).");
        }
    }

    public void StartSpawningObjects()
    {
        InvokeRepeating("SpawnObject", 2.0f, spawnRateSeconds);
    }

    public void StopSpawningObjects()
    {
        CancelInvoke();
    }

    public void SpawnObject()
    {
        float spawnOffsetX = Random.Range(-spawnArea, spawnArea);
        float spawnOffsetY = Random.Range(-spawnArea, spawnArea);
        Vector3 newPosition = new Vector3(transform.position.x + spawnOffsetX, transform.position.y, transform.position.z + spawnOffsetY);

        if (spawnedPrefabsPool.Count < amountToPool)
        {
            GameObject go = Instantiate(spawnablePrefab, newPosition, transform.rotation, transform);
            go.GetComponent<FallingObject>().bucketGO = bucketGO;
            spawnedPrefabsPool.Add(go);
        }
        else
        {
            GameObject pooledGO = GetPooledObject();
            pooledGO.transform.position = newPosition;
            pooledGO.SetActive(true);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!spawnedPrefabsPool[i].activeInHierarchy)
            {
                return spawnedPrefabsPool[i];
            }
        }
        return null;
    }


    public void ClearSpawnedObjectsPool()
    {
        foreach (var spawnedGO in spawnedPrefabsPool)
        {
            Destroy(spawnedGO);
        }

        spawnedPrefabsPool.Clear();
    }
}
