using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject skeletonPrefab;
    public GameObject bearPrefab;
    public GameObject MagePreFab;
    public GameObject DocPreFab;
    public GameObject MinoPreFab;
    public GameObject BoxPreFab;
    public Vector3 spawnAreaCenter = Vector3.zero;
    public Vector3 spawnAreaSize = new Vector3(10f, 0f, 10f);
    // Current difficulty (the total worth of enemies)
    public int currentDifficulty = -1;

    private bool spawnEnabled = false;


    public void Start()
    {
        Debug.LogWarning("currentDifficulty" + currentDifficulty);
    }
    private void Update()
    {
        if (spawnEnabled)
        {
            SpawnEnemies();
            spawnEnabled = false;
        }
    }

    public void StartSpawning()
    {
        spawnEnabled = true;
    }

    void SpawnEnemies()
    {
        currentDifficulty += 2;
        Debug.LogWarning("currentDifficulty" + currentDifficulty);
        int remainingWorth = currentDifficulty;

        while (remainingWorth > 0)
        {
            GameObject enemyPrefab;

            float randomValue = Random.value;
            Vector3 spawnPosition = GetRandomSpawnPosition();
            if (randomValue < 0.33f)
            {
                spawnPosition = GetRandomSpawnPosition();
                GameObject box = Instantiate(BoxPreFab, spawnPosition, Quaternion.identity);
            }



            //adding more enemy just need to change the % to 1 / number of Enemey Prefabs
            //the second statment check for the worth, check highest worth first
            if (randomValue < 0.20f && remainingWorth >= 10)
            {
                enemyPrefab = bearPrefab;
            }
            else if(randomValue < 0.40f && remainingWorth >= 3)
            {
                enemyPrefab = MagePreFab;
            }
            else if (randomValue < 0.60f && remainingWorth >= 4)
            {
                enemyPrefab = DocPreFab;
            }
            else if(randomValue < 0.75f && remainingWorth >= 15)
            {
                enemyPrefab = MinoPreFab;
            }
            else
            {
                enemyPrefab = skeletonPrefab;
            }

                // Get the worth of the enemy
                int enemyWorth = GetEnemyWorth(enemyPrefab);

            // Check if adding this enemy will exceed the total worth, if so, adjust the worth
            if (remainingWorth - enemyWorth < 0)
            {
                break; // Stop spawning if adding this enemy would exceed the total worth
            }

            // Spawn the enemy
            
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

            // Update the remaining worth
            remainingWorth -= enemyWorth;
            Debug.LogWarning("remainingWorth" + remainingWorth);
        }
    }

    int GetEnemyWorth(GameObject enemyPrefab)
    {
        // Get the worth of the enemy based on its attack script
        if (enemyPrefab.GetComponent<SkeletonAttack>() != null)
        {
            return enemyPrefab.GetComponent<SkeletonAttack>().Worth;
        }
        else if (enemyPrefab.GetComponent<BearAttack>() != null)
        {
            return enemyPrefab.GetComponent<BearAttack>().Worth;
        }
        else if(enemyPrefab.GetComponent<MageAttack>() != null)
        {
            return enemyPrefab.GetComponent<MageAttack>().Worth;
        }
        else if (enemyPrefab.GetComponent<MinoAttack>() != null)
        {
            return enemyPrefab.GetComponent<MinoAttack>().Worth;
        }
        else if (enemyPrefab.GetComponent<DocAttack>() != null)
        {
            return enemyPrefab.GetComponent<DocAttack>().Worth;
        }
        // Default worth if the enemy type is unknown
        return 1;
    }



    Vector3 GetRandomSpawnPosition()
    {
        float randomX = Random.Range(spawnAreaCenter.x - spawnAreaSize.x / 2, spawnAreaCenter.x + spawnAreaSize.x / 2);
        float randomZ = Random.Range(spawnAreaCenter.z - spawnAreaSize.z / 2, spawnAreaCenter.z + spawnAreaSize.z / 2);

        return new Vector3(randomX, 2f, randomZ);
    }
}
