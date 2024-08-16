using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [Header(" Elements ")]
    private HealthManager healthManager;
    private BuildingTypeSO buildingType;


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
}
