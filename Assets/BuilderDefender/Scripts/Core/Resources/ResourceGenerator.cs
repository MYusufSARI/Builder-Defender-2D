using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    [Header(" Data ")]
    private ResourceGeneratorData resourceGeneratorData;


    [Header(" Settings ")]
    private float _timer;
    private float timerMax;



    public static int GetNearbyResourceAmount(ResourceGeneratorData resourceGeneratorData, Vector3 position)
    {
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(position, resourceGeneratorData.resourceDetectionRadius);

        int nearbyResourceAmount = 0;

        foreach (Collider2D collider2D in collider2DArray)
        {
            ResourceNode resourceNode = collider2D.GetComponent<ResourceNode>();

            if (resourceNode != null)
            {
                // It's a resource node!

                if (resourceNode.resourceType == resourceGeneratorData.resourceType)
                {
                    // Same type!
                    nearbyResourceAmount++;
                }
            }
        }

        nearbyResourceAmount = Mathf.Clamp(nearbyResourceAmount, 0, resourceGeneratorData.maxResourceAmount);

        return nearbyResourceAmount;
    }



    private void Awake()
    {
        resourceGeneratorData = GetComponent<BuildingTypeHolder>().buildingType.resourceGeneratorData;

        timerMax = resourceGeneratorData.timerMax;
    }


    private void Start()
    {
        int nearbyResourceAmount = GetNearbyResourceAmount(resourceGeneratorData, transform.position);

        if (nearbyResourceAmount == 0)
        {
            // No reaouse nodes nearby
            // Disable resource generator
            enabled = false;
        }

        else
        {
            timerMax = (resourceGeneratorData.timerMax / 2f) +
                resourceGeneratorData.timerMax *
                (1 - (float)nearbyResourceAmount / resourceGeneratorData.maxResourceAmount);
        }
    }


    private void Update()
    {
        _timer -= Time.deltaTime;

        if (_timer <= 0f)
        {
            _timer += timerMax;

            ResourceManager.Instance.AddResource(resourceGeneratorData.resourceType, 1);
        }
    }


    public ResourceGeneratorData GetResourceGeneratorData()
    {
        return resourceGeneratorData;
    }


    public float GetTimerNormalized()
    {
        return _timer / timerMax;
    }


    public float GetAmountGeneratedPerSecond()
    {
        return 1 / timerMax;
    }

}
