using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Meta.XR.MRUtilityKit;

public class OrbSpawner : MonoBehaviour
{
    public int OrbsToSpawn = 5;            // Total number of orbs to spawn
    public GameObject orbPrefab;            // Orb prefab reference
    public float height;                    // Height to spawn the orb at
    public List<GameObject> spawnedOrbs;    // List to keep track of spawned orbs

    public int maxTry = 100;                // Maximum number of attempts for orb placement
    public int currentTry = 0;              // Current number of attempts

    public static OrbSpawner instance;      // Singleton instance

    private void Awake()
    {
        instance = this;                    // Set singleton instance
    }

    void Start()
    {
        MRUK.Instance.RegisterSceneLoadedCallback(SpawnOrbs);  // Register callback to spawn orbs after the scene loads
    }

    // Destroy an orb and check if all orbs are collected before resetting the scene
    public void DestroyOrb(GameObject orb)
    {
        spawnedOrbs.Remove(orb);            // Remove the orb from the list
        Destroy(orb);                       // Destroy the orb

        // Reset the scene only when all orbs are collected
        if (spawnedOrbs.Count == 0)
        {
            // All orbs collected, reset the scene
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }

    // Spawn orbs on random positions on the floor
    public void SpawnOrbs()
    {
        // Clear the orb list to reset the tracking
        spawnedOrbs = new List<GameObject>();

        for (int i = 0; i < OrbsToSpawn; i++)
        {
            Vector3 randomPosition = Vector3.zero;

            MRUKRoom room = MRUK.Instance.GetCurrentRoom();

            // Reset the number of attempts before each orb spawn
            currentTry = 0;

            // Try to find a valid position to spawn the orb
            while (currentTry < maxTry)
            {
                bool hasFound = room.GenerateRandomPositionOnSurface(
                    MRUK.SurfaceType.FACING_UP, 1,
                    LabelFilter.Included(MRUKAnchor.SceneLabels.FLOOR),
                    out randomPosition, out Vector3 n);

                if (hasFound)
                    break;

                currentTry++;
            }

            // Adjust the height of the orb's spawn position
            randomPosition.y = height;

            // Instantiate the orb at the calculated position
            GameObject spawned = Instantiate(orbPrefab, randomPosition, Quaternion.identity);

            // Add the spawned orb to the tracking list
            spawnedOrbs.Add(spawned);
        }
    }
}
