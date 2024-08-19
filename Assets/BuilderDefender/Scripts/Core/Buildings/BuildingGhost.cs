using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGhost : MonoBehaviour
{
    [Header(" Elements ")]
    private GameObject spriteGameObject;


    [Header(" Data ")]
    private ResourceNearbyOverlay resourceNearbyOverlay;


    [Header(" Consts ")]
    private const string SPRİTE = "Sprite";
    private const string RESOURCE_NEARBY_OVERLAY = "PF_ResourceNearbyOverlay";



    private void Awake()
    {
        spriteGameObject = transform.Find(SPRİTE).gameObject;

        resourceNearbyOverlay = transform.Find(RESOURCE_NEARBY_OVERLAY).GetComponent<ResourceNearbyOverlay>();

        Hide();
    }


    private void Start()
    {
        BuildingManager.onActiveBuildingTypeChanged += OnActiveBuildingTypeChangedCallback;
    }


    private void OnDestroy()
    {
        BuildingManager.onActiveBuildingTypeChanged -= OnActiveBuildingTypeChangedCallback;
    }


    private void OnActiveBuildingTypeChangedCallback(BuildingManager.OnActiveBuildingTypeChangedEvent onActiveBuildingTypeChangedEvent)
    {
        if (onActiveBuildingTypeChangedEvent.activeBuildingType == null)
        {
            Hide();
            resourceNearbyOverlay.Hide();
        }

        else
        {
            Show(onActiveBuildingTypeChangedEvent.activeBuildingType._sprite);

            if (onActiveBuildingTypeChangedEvent.activeBuildingType.hasResourceGeneratorData)
            {
                resourceNearbyOverlay.Show(onActiveBuildingTypeChangedEvent.activeBuildingType.resourceGeneratorData);
            }

            else
            {
                resourceNearbyOverlay.Hide();
            }

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
