using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveManager : MonoBehaviour
{

    [Header(" Settings ")]
    private float nextWaveSpawnTimer;
    private float nextEnemySpawnTimer;
    private int remainingEnemySpawnAmount;
    private Vector3 spawnPosition;


    private void Start()
    {
        nextWaveSpawnTimer = 3f;
    }


    private void Update()
    {
        nextWaveSpawnTimer -= Time.deltaTime;

        if (nextWaveSpawnTimer < 0f)
        {
            SpawnWave();
        }

        if (remainingEnemySpawnAmount >0)
        {
            nextEnemySpawnTimer -= Time.deltaTime;

            if (nextEnemySpawnTimer < 0f)
            {
                nextEnemySpawnTimer = Random.Range(0f, 0.2f);

                Enemy.Create(spawnPosition + UtilsClass.GetRandomDirection() * Random.Range(0f, 10f));
                remainingEnemySpawnAmount--;
            }
        }
    }


    private void SpawnWave()
    {
        spawnPosition = new Vector3(40, 0);

        nextWaveSpawnTimer = 10f;

        remainingEnemySpawnAmount = 10;
    }
}
