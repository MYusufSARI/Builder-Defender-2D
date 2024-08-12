using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingTypeSelectUI : MonoBehaviour
{
    [Header(" UI Elements ")]
    [SerializeField] private Sprite arrowSprite;
    private Transform arrowButton;


    [Header(" Dictionary ")]
    private Dictionary<BuildingTypeSO, Transform> buttonTransformDictionary;


    [Header(" Consts ")]
    private const string BUILDING_IMAGE = "BuildingImage";
    private const string BUTTON_TEMPLATE = "ButtonTemplate";
    private const string SELECTED = "Selected";



    private void Awake()
    {
        Transform buttonTemplate = transform.Find(BUTTON_TEMPLATE);

        buttonTemplate.gameObject.SetActive(false);

        BuildingTypeListSO buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);

        buttonTransformDictionary = new Dictionary<BuildingTypeSO, Transform>();

        int index = 0;


        arrowButton = Instantiate(buttonTemplate, transform);
        arrowButton.gameObject.SetActive(true);

        float offsetAmount = 120f;
        arrowButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0);

        arrowButton.Find(BUILDING_IMAGE).GetComponent<Image>().sprite = arrowSprite;
        arrowButton.Find(BUILDING_IMAGE).GetComponent<RectTransform>().sizeDelta = new Vector2(0, -40);

        arrowButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            BuildingManager.Instance.SetActiveBuildingType(null);
        });

        index++;


        foreach (BuildingTypeSO buildingType in buildingTypeList._list)
        {
            Transform buttonTransform = Instantiate(buttonTemplate, transform);
            buttonTransform.gameObject.SetActive(true);

            offsetAmount = 120f;
            buttonTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0);
            buttonTransform.Find(BUILDING_IMAGE).GetComponent<Image>().sprite = buildingType._sprite;

            buttonTransform.GetComponent<Button>().onClick.AddListener(() =>
            {
                BuildingManager.Instance.SetActiveBuildingType(buildingType);
            });

            buttonTransformDictionary[buildingType] = buttonTransform;

            index++;
        }
    }


    private void Update()
    {
        UpdateActiveBuildingTypeButton();
    }


    private void UpdateActiveBuildingTypeButton()
    {
        arrowButton.Find(SELECTED).gameObject.SetActive(false);

        foreach (BuildingTypeSO buildingType in buttonTransformDictionary.Keys)
        {
            Transform buttonTransform = buttonTransformDictionary[buildingType];

            buttonTransform.Find(SELECTED).gameObject.SetActive(false);
        }

        BuildingTypeSO activeBuildingType = BuildingManager.Instance.GetActiveBuildingType();


        if (activeBuildingType == null)
        {
            arrowButton.Find(SELECTED).gameObject.SetActive(true);
        }
        else
        {
            buttonTransformDictionary[activeBuildingType].Find(SELECTED).gameObject.SetActive(true);
        }
    }
}
