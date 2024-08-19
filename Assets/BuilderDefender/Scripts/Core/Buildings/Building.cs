using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [Header(" Elements ")]
    private Transform buildingDemolishButton;


    [Header(" Data ")]
    private HealthManager healthManager;
    private BuildingTypeSO buildingType;


    [Header(" Consts ")]
    private const string PF_BUILDING_DEMOLISH_BUTTON = "PF_BuildingDemolishButton";




    private void Awake()
    {
        buildingDemolishButton = transform.Find(PF_BUILDING_DEMOLISH_BUTTON);

        HideDemolishButton();
    }


    private void Start()
    {
        buildingType = GetComponent<BuildingTypeHolder>().buildingType;

        healthManager = GetComponent<HealthManager>();

        healthManager.SetHealthAmountMax(buildingType.healthAmountMax, true);

        healthManager.OnDied += OnDiedCallback;
    }

    private void OnDestroy()
    {
        healthManager.OnDied -= OnDiedCallback;
    }


    private void OnDiedCallback(object sender, EventArgs e)
    {
        Destroy(gameObject);
    }


    private void OnMouseEnter()
    {
        ShowBuildingDemolishButton();
    }


    private void OnMouseExit()
    {
        HideDemolishButton();
    }


    private void ShowBuildingDemolishButton()
    {
        if (buildingDemolishButton != null)
        {
            buildingDemolishButton.gameObject.SetActive(true);
        }
    }


    private void HideDemolishButton()
    {
        if (buildingDemolishButton != null)
        {
            buildingDemolishButton.gameObject.SetActive(false);
        }
    }
}
