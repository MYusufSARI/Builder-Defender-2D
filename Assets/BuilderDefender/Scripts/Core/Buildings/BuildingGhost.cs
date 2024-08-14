using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGhost : MonoBehaviour
{
    [Header(" Elements ")]
    private GameObject spriteGameObject;


    [Header(" Consts ")]
    private const string SPRİTE = "Sprite";



    private void Awake()
    {
        spriteGameObject = transform.Find(SPRİTE).gameObject;

        Hide();
    }


    private void Start()
    {
        BuildingManager.onActiveBuildingTypeChanged += OnActiveBuildingTypeChangedCallback;
    }


    private void OnActiveBuildingTypeChangedCallback(BuildingManager.OnActiveBuildingTypeChangedEvent onActiveBuildingTypeChangedEvent)
    {
        if (onActiveBuildingTypeChangedEvent.activeBuildingType == null)
        {
            Hide();
        }

        else
        {
            Show(onActiveBuildingTypeChangedEvent.activeBuildingType._sprite);
        }
    }


    private void Update()
    {
        transform.position = UtilsClass.GetMouseWorldPosition();
    }


    private void Show(Sprite ghostSprite)
    {
        spriteGameObject.SetActive(true);

        spriteGameObject.GetComponent<SpriteRenderer>().sprite = ghostSprite;
    }


    private void Hide()
    {
        spriteGameObject.SetActive(false);
    }
}
