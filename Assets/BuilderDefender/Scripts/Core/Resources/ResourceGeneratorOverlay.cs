using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceGeneratorOverlay : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private ResourceGenerator resourceGenerator;


    [Header(" Consts ")]
    private const string GOLD_ICON = "GoldIcon";
    private const string BAR = "Bar";
    private const string TEXT = "Text";


    [Header(" Settings ")]
    private Transform barTransform;


    private void Start()
    {
        ResourceGeneratorData resourceGeneratorData = resourceGenerator.GetResourceGeneratorData();

        barTransform = transform.Find(BAR);

        transform.Find(GOLD_ICON).GetComponent<SpriteRenderer>().sprite = resourceGeneratorData.resourceType._sprite;

        transform.Find(TEXT).GetComponent<TextMeshPro>().SetText(resourceGenerator.GetAmountGeneratedPerSecond().ToString("F1"));
    }


    private void Update()
    {
        barTransform.localScale = new Vector3(1- resourceGenerator.GetTimerNormalized(), 1, 1);
    }
}
