using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    [Header(" Elements ")]
    private ResourceGeneratorData resourceGeneratorData;


    [Header(" Settings ")]
    private float _timer;
    private float timerMax;



    private void Awake()
    {
        resourceGeneratorData = GetComponent<BuildingTypeHolder>().buildingType.resourceGeneratorData;

        timerMax = resourceGeneratorData.timerMax;
    }


    private void Start()
    {
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, resourceGeneratorData.resourceDetectionRadius);

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

        Debug.Log("NearbyResourceAmount: " + nearbyResourceAmount + "; timerMax: " + timerMax);
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

}
