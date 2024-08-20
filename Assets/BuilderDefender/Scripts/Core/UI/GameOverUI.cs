using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    public static GameOverUI Instance { get; private set; }

    [Header(" Consts ")]
    private const string RETRY_BUTTON = "RetryButton";
    private const string MAINMENU_BUTTON = "MainMenuButton";
    private const string GAMEOVER_MESSAGE_TEXT = "GameOverMessageText";



    private void Awake()
    {
        Instance = this;

        transform.Find(RETRY_BUTTON).GetComponent<Button>().onClick.AddListener(() =>
        {
            GameSceneManager.Load(GameSceneManager.Scene.GameScene);
        });

        transform.Find(MAINMENU_BUTTON).GetComponent<Button>().onClick.AddListener(() =>
        {
            GameSceneManager.Load(GameSceneManager.Scene.MainMenuScene);
        });

        Hide();
    }


    public void Show()
    {
        gameObject.SetActive(true);

        transform.Find(GAMEOVER_MESSAGE_TEXT).GetComponent<TextMeshProUGUI>().
            SetText("You Survived " + EnemyWaveManager.Instance.GetWaveNumber() + " Waves!");
    }


    private void Hide()
    {
        gameObject.SetActive(false);

    }
}
