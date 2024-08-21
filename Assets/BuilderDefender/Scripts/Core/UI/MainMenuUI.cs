using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [Header(" Consts ")]
    private const string PLAY_BUTTON = "PlayButton";
    private const string QUIT_BUTTON= "QuitButton";



    private void Awake()
    {

        transform.Find(PLAY_BUTTON).GetComponent<Button>().onClick.AddListener(() =>
        {
            GameSceneManager.Load(GameSceneManager.Scene.GameScene);
        });

        transform.Find(QUIT_BUTTON).GetComponent<Button>().onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
}
