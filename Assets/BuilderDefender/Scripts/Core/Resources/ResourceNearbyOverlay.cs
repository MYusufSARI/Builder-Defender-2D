using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ResourceNearbyOverlay : MonoBehaviour
{
    [Header(" Elements ")]
    private ResourceGeneratorData resourceGeneratorData;


    [Header(" Consts ")]
    private const string TEXT = "Text";
    private const string GOLD_ICON = "GoldIcon";



    private void Awake()
    {
        Hide();
    }


    private void Update()
    {
        ConvertPercentage();
    }


    private void ConvertPercentage()
    {
        int nearbyResourceAmount = ResourceGenerator.GetNearbyResourceAmount(resourceGeneratorData, transform.position - transform.localPosition);
        float percent = Mathf.RoundToInt((float)nearbyResourceAmount / resourceGeneratorData.maxResourceAmount * 100f);

        transform.Find(TEXT).GetComponent<TextMeshPro>().SetText(percent + "%");
    }


    public void Show(ResourceGeneratorData resourceGeneratorData)
    {
        this.resourceGeneratorData = resourceGeneratorData;

        gameObject.SetActive(true);

        transform.Find(GOLD_ICON).GetComponent<SpriteRenderer>().sprite = resourceGeneratorData.resourceType._sprite;
    }


    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
