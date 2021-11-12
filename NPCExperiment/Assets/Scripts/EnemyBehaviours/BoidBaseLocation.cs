using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidBaseLocation : MonoBehaviour
{
    public BoidHomeData homeData;

    public BoidHomeData baseType
    {
        get { return homeData; }
        set 
        { 
            homeData = value; 
        }
    } 
    public float min = 2f;
    public float max = 3f;
    public float speed = 1f;

    // The GameObject to instantiate.
    public GameObject boidToSpawn;

    // An instance of the ScriptableObject defined above.
    

    // This will be appended to the name of the created entities and increment when each is created.
    int instanceNumber = 1;

    void Start()
    {
        homeData.SetMovement(gameObject);

        boidToSpawn = homeData.objectToSpawn;

        foreach(Transform child in this.transform)
        {
            if (child != null)
            {
                Destroy(child.gameObject);
            }
        }

        min = transform.position.x;
        max = transform.position.x + 3;

        SpawnEntities();
    }

    void Update()
    {
        homeData.SetMovement(gameObject);
    }

    void SpawnEntities()
    {
        for (int i = 0; i < homeData.numberOfPrefabsToCreate; i++)
        {
            // Creates an instance of the prefab at the current spawn point.
            GameObject currentEntity = Instantiate(homeData.objectToSpawn, transform.position, Quaternion.identity, transform);


            // Sets the name of the instantiated entity to be the string defined in the ScriptableObject and then appends it with a unique number. 
            currentEntity.name = homeData.boidData.prefabName + instanceNumber;

            // Moves to the next spawn point index. If it goes out of range, it wraps back to the start.
            //currentSpawnPointIndex = (currentSpawnPointIndex + 1) % spawnManagerValues.spawnPoints.Length;

            instanceNumber++;
        }
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawLine(transform.position.x, transform.position.x+3);
    }
}
