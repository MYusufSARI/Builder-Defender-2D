using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveManager : MonoBehaviour
{
    private enum State
    {
        WaitingToSpawnNextWave,
        SpawningWave,
    }

    [Header(" State ")]
    private State _state;


    [Header(" Elements ")]
    [SerializeField] private List<Transform> spawnPositionTransformList;
    [SerializeField] private Transform nextWaveSpawnPosition;
    private Vector3 spawnPosition;


    [Header(" Settings ")]
    private float nextWaveSpawnTimer;
    private float nextEnemySpawnTimer;
    private int remainingEnemySpawnAmount;
    private int waveNumber;



    private void Start()
    {
        _state = State.WaitingToSpawnNextWave;

        spawnPosition = spawnPositionTransformList[Random.Range(0, spawnPositionTransformList.Count)].position;
        nextWaveSpawnPosition.position = spawnPosition;

        nextWaveSpawnTimer = 3f;
    }


    private void Update()
    {
        ManageState();
    }


    private void ManageState()
    {
        switch (_state)
        {
            case State.WaitingToSpawnNextWave:
                nextWaveSpawnTimer -= Time.deltaTime;

                if (nextWaveSpawnTimer < 0f)
                {
                    SpawnWave();
                }
                break;

            case State.SpawningWave:
                if (remainingEnemySpawnAmount > 0)
                {
                    nextEnemySpawnTimer -= Time.deltaTime;

                    if (nextEnemySpawnTimer < 0f)
                    {
                        nextEnemySpawnTimer = Random.Range(0f, 0.2f);

                        Enemy.Create(spawnPosition + UtilsClass.GetRandomDirection() * Random.Range(0f, 10f));
                        remainingEnemySpawnAmount--;

                        if (remainingEnemySpawnAmount <= 0)
                        {
                            _state = State.WaitingToSpawnNextWave;

                            spawnPosition = spawnPositionTransformList[Random.Range(0, spawnPositionTransformList.Count)].position;
                        }
                    }
                }
                break;
        }
    }


    private void SpawnWave()
    {
        nextWaveSpawnTimer = 10f;

        remainingEnemySpawnAmount = 5 + 3 * waveNumber;

        _state = State.SpawningWave;

        waveNumber++;
    }
}
