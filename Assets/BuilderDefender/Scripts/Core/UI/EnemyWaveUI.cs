using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyWaveUI : MonoBehaviour
{
    [Header(" Elements ")]
    private TextMeshProUGUI waveNumberText;
    private TextMeshProUGUI waveMessageText;
    private RectTransform enemyWaveSpawnIndicator;
    private RectTransform enemyClosestSpawnPositionIndicator;

    private Camera mainCamera;


    [Header(" Data ")]
    [SerializeField] private EnemyWaveManager enemyWaveManager;


    [Header(" Consts ")]
    private const string WAVE_NUMBER_TEXT = "WaveNumberText";
    private const string WAVE_MESSAGE_TEXT = "WaveMessageText";
    private const string ENEMY_WAVE_SPAWN_INDICATOR = "EnemyWaveSpawnIndicator";
    private const string ENEMY_CLOSEST_POSITION_INDICATOR = "EnemyClosestPositionIndicator";



    private void Awake()
    {
        waveNumberText = transform.Find(WAVE_NUMBER_TEXT).GetComponent<TextMeshProUGUI>();
        waveMessageText = transform.Find(WAVE_MESSAGE_TEXT).GetComponent<TextMeshProUGUI>();
        enemyWaveSpawnIndicator = transform.Find(ENEMY_WAVE_SPAWN_INDICATOR).GetComponent<RectTransform>();
        enemyClosestSpawnPositionIndicator = transform.Find(ENEMY_CLOSEST_POSITION_INDICATOR).GetComponent<RectTransform>();
    }


    private void Start()
    {
        mainCamera = Camera.main;

        EnemyWaveManager.onWaveNumberChanged += WaveNumberChangedCallback;

        SetWaveNumberText("Wave " + enemyWaveManager.GetWaveNumber());
    }


    private void OnDestroy()
    {
        EnemyWaveManager.onWaveNumberChanged -= WaveNumberChangedCallback;
    }


    private void WaveNumberChangedCallback()
    {
        SetWaveNumberText("Wave " + enemyWaveManager.GetWaveNumber());
    }


    private void Update()
    {
        ManageNextWaveMessage();

        ManageEnemyWaveSpawnPosition();

        ManageEnemyClosestPosition();
    }


    private void ManageNextWaveMessage()
    {
        float nextWaveSpawnTimer = enemyWaveManager.GetNextWaveSpawnTimer();

        if (nextWaveSpawnTimer <= 0f)
        {
            SetMessageText("");
        }

        else
        {
            SetMessageText("Next Wave in " + nextWaveSpawnTimer.ToString("F1") + "s");
        }
    }


    private void ManageEnemyWaveSpawnPosition()
    {
        Vector3 directionToNextSpawnPosition = (enemyWaveManager.GetSpawnPosition() - mainCamera.transform.position).normalized;

        enemyWaveSpawnIndicator.anchoredPosition = directionToNextSpawnPosition * 300f;
        enemyWaveSpawnIndicator.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVector(directionToNextSpawnPosition));

        float distanceToNextSpawnPosition = Vector3.Distance(enemyWaveManager.GetSpawnPosition(), mainCamera.transform.position);
        enemyWaveSpawnIndicator.gameObject.SetActive(distanceToNextSpawnPosition > mainCamera.orthographicSize * 1.5f);
    }


    private void ManageEnemyClosestPosition()
    {
        float targetMaxRadius = 9999f;
        
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(mainCamera.transform.position, targetMaxRadius);

        Enemy targetEnemy = null;

        foreach (Collider2D collider2D in collider2DArray)
        {
            Enemy enemy = collider2D.GetComponent<Enemy>();

            if (enemy != null)
            {
                // It's a enemy!
                if (targetEnemy == null)
                {
                    targetEnemy = enemy;
                }
                else
                {
                    if (Vector3.Distance(transform.position, enemy.transform.position) <
                        Vector3.Distance(transform.position, targetEnemy.transform.position))
                    {
                        // Closer!
                        targetEnemy = enemy;
                    }
                }
            }
        }

        if (targetEnemy != null)
        {
            Vector3 directionToClosestEnemy = (targetEnemy.transform.position - mainCamera.transform.position).normalized;

            enemyClosestSpawnPositionIndicator.anchoredPosition = directionToClosestEnemy * 250;
            enemyClosestSpawnPositionIndicator.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVector(directionToClosestEnemy));

            float distanceToClosestEnemy = Vector3.Distance(targetEnemy.transform.position, mainCamera.transform.position);
            enemyClosestSpawnPositionIndicator.gameObject.SetActive(distanceToClosestEnemy > mainCamera.orthographicSize * 1.5f);
        }

        else
        {
            // NO enemies alives
            enemyClosestSpawnPositionIndicator.gameObject.SetActive(false);
        }
    }


    private void SetMessageText(string message)
    {
        waveMessageText.SetText(message);
    }


    private void SetWaveNumberText(string text)
    {
        waveNumberText.SetText(text);
    }
}
