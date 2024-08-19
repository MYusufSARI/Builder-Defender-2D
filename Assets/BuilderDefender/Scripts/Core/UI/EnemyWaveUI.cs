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
    private Camera mainCamera;


    [Header(" Data ")]
    [SerializeField] private EnemyWaveManager enemyWaveManager;


    [Header(" Consts ")]
    private const string WAVE_NUMBER_TEXT = "WaveNumberText";
    private const string WAVE_MESSAGE_TEXT = "WaveMessageText";
    private const string ENEMY_WAVE_SPAWN_INDICATOR = "EnemyWaveSpawnIndicator";



    private void Awake()
    {
        waveNumberText = transform.Find(WAVE_NUMBER_TEXT).GetComponent<TextMeshProUGUI>();
        waveMessageText = transform.Find(WAVE_MESSAGE_TEXT).GetComponent<TextMeshProUGUI>();
        enemyWaveSpawnIndicator = transform.Find(ENEMY_WAVE_SPAWN_INDICATOR).GetComponent<RectTransform>();
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
        ManageSpawnTimer();
    }


    private void ManageSpawnTimer()
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

        Vector3 directionToNextSpawnPosition = (enemyWaveManager.GetSpawnPosition() - mainCamera.transform.position).normalized;

        enemyWaveSpawnIndicator.anchoredPosition = directionToNextSpawnPosition * 300f;
        enemyWaveSpawnIndicator.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVector(directionToNextSpawnPosition));

        float distanceToNextSpawnPosition = Vector3.Distance(enemyWaveManager.GetSpawnPosition(), mainCamera.transform.position);
        enemyWaveSpawnIndicator.gameObject.SetActive(distanceToNextSpawnPosition > mainCamera.orthographicSize * 1.5f);
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
