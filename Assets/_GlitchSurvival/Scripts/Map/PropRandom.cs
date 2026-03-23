using UnityEngine;
using System.Collections.Generic;

public class PropRandom : MonoBehaviour
{
    public List<GameObject> propsSpawnPoints;
    public List<GameObject> propsPrefabs;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnProps();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SpawnProps()
    {
        foreach(GameObject spawnPoint in propsSpawnPoints)
        {
            int randomIndex = Random.Range(0, propsPrefabs.Count);
            GameObject prop = Instantiate(propsPrefabs[randomIndex], spawnPoint.transform.position, Quaternion.identity);
            prop.transform.parent = spawnPoint.transform;
        }
    }
}
