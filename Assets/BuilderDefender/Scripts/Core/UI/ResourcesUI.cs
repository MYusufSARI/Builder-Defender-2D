using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResourcesUI : MonoBehaviour
{
    [Header(" Elements ")]
    private ResourceTypeListSO resourceTypeList;
    private Dictionary<ResourceTypeSO, Transform> resourceTypeDictionary;


    [Header(" Consts ")]
    private const string RESOURCE_TEMPLATE = "ResourceTemplate";
    private const string RESOURCE_IMAGE = "ResourceImage";
    private const string RESOURCE_TEXT = "ResourceText";



    private void Awake()
    {
        resourceTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);

        resourceTypeDictionary = new Dictionary<ResourceTypeSO, Transform>();

        Transform resourceTemplate = transform.Find(RESOURCE_TEMPLATE);

        resourceTemplate.gameObject.SetActive(false);

        int index = 0;

        foreach (ResourceTypeSO resourceType in resourceTypeList._list)
        {
            Transform resourceTransform = Instantiate(resourceTemplate, transform);

            resourceTransform.gameObject.SetActive(true);

            float offsetAmount = -120f;
            resourceTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0);

            resourceTransform.Find(RESOURCE_IMAGE).GetComponent<Image>().sprite = resourceType._sprite;

            resourceTypeDictionary[resourceType] = resourceTransform;
            
            index++;
        }
    }


    private void Start()
    {
        ResourceManager.onResourceAmountChanged += ResourceAmountChangedCallback;

        UpdateResourceAmount();
    }


    private void ResourceAmountChangedCallback()
    {
        UpdateResourceAmount();
    }


    private void UpdateResourceAmount()
    {
        foreach (ResourceTypeSO resourceType in resourceTypeList._list)
        {
            Transform resourceTransform = resourceTypeDictionary[resourceType];

            int resourceAmount = ResourceManager.Instance.GetResourceAmount(resourceType);
            //resourceTransform.Find(RESOURCE_TEXT).GetComponent<TextMeshProUGUI>().SetText(resourceAmount.ToString());
        }
    }
}
