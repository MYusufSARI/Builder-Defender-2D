using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    [Header(" Elements ")]
    private BuildingTypeSO buildingType;


    [Header(" Settings ")]
    private float _timer;
    private float timerMax;



    private void Awake()
    {
        buildingType = GetComponent<BuildingTypeHolder>().buildingType;

        timerMax = buildingType.resourceGeneratorData.timerMax;
    }


    private void Update()
    {
        _timer -= Time.deltaTime;

        if (_timer <= 0f)
        {
            _timer += timerMax;

            ResourceManager.Instance.AddResource(buildingType.resourceGeneratorData.resourceType, 1);
        }
    }

}
