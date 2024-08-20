using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    [Header(" Consts ")]
    private const string VOLUME_UP_BUTTON = "VolumeUpButton";
    private const string VOLUME_DOWN_BUTTON = "VolumeDownButton";
    private const string VOLUME_VALUE_TEXT = "VolumeValueText";


    [Header(" Settings ")]
    private float _volume = 0.5f;


    [Header(" Elements ")]
    private TextMeshProUGUI musicText;


    [Header(" Data ")]
    [SerializeField] private MusicManager musicManager;


    private void Awake()
    {
        musicText = transform.Find(VOLUME_VALUE_TEXT).GetComponent<TextMeshProUGUI>();

        transform.Find(VOLUME_UP_BUTTON).GetComponent<Button>().onClick.AddListener(() =>
        {
            musicManager.IncreaseVolume();

            UpdateText();
        });

        transform.Find(VOLUME_DOWN_BUTTON).GetComponent<Button>().onClick.AddListener(() =>
        {
            musicManager.DecreaseVolume();

            UpdateText(); 
        });
    }


    private void Start()
    {
        UpdateText();
    }


    private void UpdateText()
    {
        musicText.SetText(Mathf.RoundToInt(musicManager.GetVolume() * 10).ToString());
    }
}
