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


    [Header(" Data ")]
    [SerializeField] private EnemyWaveManager enemyWaveManager;


    [Header(" Consts ")]
    private const string WAVE_NUMBER_TEXT = "WaveNumberText";
    private const string WAVE_MESSAGE_TEXT = "WaveMessageText";



    private void Awake()
    {
        waveNumberText =  transform.Find(WAVE_NUMBER_TEXT).GetComponent<TextMeshProUGUI>();
        waveMessageText =  transform.Find(WAVE_MESSAGE_TEXT).GetComponent<TextMeshProUGUI>();
    }


    private void Start()
    {
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
