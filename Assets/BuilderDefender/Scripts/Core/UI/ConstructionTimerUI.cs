using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConstructionTimerUI : MonoBehaviour
{
    [Header(" Elements ")]
    private Image constructionProgressImage;


    [Header(" Data ")]
    [SerializeField] private BuildingConstruction buildingConstruction;


    [Header("Consts")]
    private const string MASK = "Mask";
    private const string IMAGE = "Image";

    private void Awake()
    {
        constructionProgressImage = transform.Find(MASK).Find(IMAGE).GetComponent<Image>();
    }


    private void Update()
    {
        constructionProgressImage.fillAmount = buildingConstruction.GetConstructionTimerNormalize();
    }
}
