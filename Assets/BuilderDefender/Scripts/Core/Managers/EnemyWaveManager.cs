using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyWaveManager : MonoBehaviour
{
    public static EnemyWaveManager Instance { get; private set; }

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


    [Header(" Events ")]
    public static Action onWaveNumberChanged;



    private void Awake()
    {
        Instance = this;
    }


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
                            nextWaveSpawnPosition.position = spawnPosition;
                            nextWaveSpawnTimer = 10f;
                        }
                    }
                }
                break;
        }
    }


    private void SpawnWave()
    {
        remainingEnemySpawnAmount = 5 + 3 * waveNumber;

        _state = State.SpawningWave;

        waveNumber++;

        onWaveNumberChanged?.Invoke();
    }


    public int GetWaveNumber()
    {
        return waveNumber;
    }


    public float GetNextWaveSpawnTimer()
    {
        return nextWaveSpawnTimer;
    }

    public Vector3 GetSpawnPosition()
    {
        return spawnPosition;
    }
}
