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


    private void Start()
    {
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, 5f);

        int nearbyResourceAmount = 0;

        foreach (Collider2D collider2D in collider2DArray)
        {
            ResourceNode resourceNode = collider2D.GetComponent<ResourceNode>();

            if (resourceNode != null)
            {
                // It's a resource node!
                nearbyResourceAmount++;
            }
        }

        Debug.Log("NearbyResourceAmount: " + nearbyResourceAmount);
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
