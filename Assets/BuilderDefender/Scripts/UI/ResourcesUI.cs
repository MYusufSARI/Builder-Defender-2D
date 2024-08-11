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

    private void Awake()
    {
        resourceTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);

        resourceTypeDictionary = new Dictionary<ResourceTypeSO, Transform>();

        Transform resourceTemplate = transform.Find("ResourceTemplate");

        resourceTemplate.gameObject.SetActive(false);

        int index = 0;

        foreach (ResourceTypeSO resourceType in resourceTypeList._list)
        {
            Transform resourceTransform = Instantiate(resourceTemplate, transform);

            resourceTransform.gameObject.SetActive(true);

            float offsetAmount = -120f;
            resourceTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0);

            resourceTransform.Find("Image").GetComponent<Image>().sprite = resourceType._sprite;

            resourceTypeDictionary[resourceType] = resourceTransform;
            
            index++;
        }
    }


    private void Start()
    {
        UpdateResourceAmount();
    }


    private void UpdateResourceAmount()
    {
        foreach (ResourceTypeSO resourceType in resourceTypeList._list)
        {
            Transform resourceTransform = resourceTypeDictionary[resourceType];

            int resourceAmount = ResourceManager.Instance.GetResourceAmount(resourceType);
            resourceTransform.Find("Text").GetComponent<TextMeshProUGUI>().SetText(resourceAmount.ToString());
        }
    }
}
