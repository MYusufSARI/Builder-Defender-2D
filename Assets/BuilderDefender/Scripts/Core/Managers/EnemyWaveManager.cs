using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveManager : MonoBehaviour
{


    private void Start()
    {
        SpawnWave();
    }


    private void SpawnWave()
    {
        Vector3 spawnPosition = new Vector3(20, 0);

        for (int i = 0; i < 10; i++)
        {
            Enemy.Create(spawnPosition + UtilsClass.GetRandomDirection() * Random.Range(0f, 10f));
        }
    }
}
