using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [Header(" Elements ")]
    private Transform buildingDemolishButton;
    private Transform buildingRepairButton;


    [Header(" Data ")]
    private HealthManager healthManager;
    private BuildingTypeSO buildingType;


    [Header(" Consts ")]
    private const string PF_BUILDING_DEMOLISH_BUTTON = "PF_BuildingDemolishButton";
    private const string PF_BUILDING_REPAIR_BUTTON = "PF_BuildingRepairButton";



    private void Awake()
    {
        buildingDemolishButton = transform.Find(PF_BUILDING_DEMOLISH_BUTTON);
        buildingRepairButton = transform.Find(PF_BUILDING_REPAIR_BUTTON);

        HideBuildingDemolishButton();
        HideBuildingRepairButton();
    }


    private void Start()
    {
        buildingType = GetComponent<BuildingTypeHolder>().buildingType;

        healthManager = GetComponent<HealthManager>();

        healthManager.SetHealthAmountMax(buildingType.healthAmountMax, true);

        healthManager.OnDied += OnDiedCallback;
        healthManager.OnDamaged += OnDamagedCallback;
        healthManager.OnHealed += OnHealedCallback;
    }

    private void OnDestroy()
    {
        healthManager.OnDied -= OnDiedCallback;
        healthManager.OnDamaged -= OnDamagedCallback;
        healthManager.OnHealed -= OnHealedCallback;
    }


    private void OnHealedCallback(object sender, EventArgs e)
    {
        if (healthManager.IsFullHealth())
        {
            HideBuildingRepairButton();
        }
    }


    private void OnDamagedCallback(object sender, EventArgs e)
    {
        ShowBuildingRepairButton();

        SoundManager.Instance.PlaySound(SoundManager.Sound.BuildingDamaged);

    }


    private void OnDiedCallback(object sender, EventArgs e)
    {
        SoundManager.Instance.PlaySound(SoundManager.Sound.BuildingDestroyed);

        Destroy(gameObject);
    }


    private void OnMouseEnter()
    {
        ShowBuildingDemolishButton();
    }


    private void OnMouseExit()
    {
        HideBuildingDemolishButton();
    }


    private void ShowBuildingDemolishButton()
    {
        if (buildingDemolishButton != null)
        {
            buildingDemolishButton.gameObject.SetActive(true);
        }
    }


    private void HideBuildingDemolishButton()
    {
        if (buildingDemolishButton != null)
        {
            buildingDemolishButton.gameObject.SetActive(false);
        }
    }


    private void ShowBuildingRepairButton()
    {
        if (buildingRepairButton != null)
        {
            buildingRepairButton.gameObject.SetActive(true);
        }
    }


    private void HideBuildingRepairButton()
    {
        if (buildingRepairButton != null)
        {
            buildingRepairButton.gameObject.SetActive(false);
        }
    }
}
