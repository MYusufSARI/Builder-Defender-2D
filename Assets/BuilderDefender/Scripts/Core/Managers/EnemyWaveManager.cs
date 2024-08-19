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
    [SerializeField] private Transform spawnPositionTransform;
    private Vector3 spawnPosition;


    [Header(" Settings ")]
    private float nextWaveSpawnTimer;
    private float nextEnemySpawnTimer;
    private int remainingEnemySpawnAmount;



    private void Start()
    {
        _state = State.WaitingToSpawnNextWave;

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
                        }
                    }
                }
                break;
        }
    }


    private void SpawnWave()
    {
        spawnPosition = spawnPositionTransform.position;

        nextWaveSpawnTimer = 10f;

        remainingEnemySpawnAmount = 10;

        _state = State.SpawningWave;
    }
}
