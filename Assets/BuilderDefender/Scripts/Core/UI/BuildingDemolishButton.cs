using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingDemolishButton : MonoBehaviour
{
    [Header(" Data ")]
    [SerializeField] private Building building;


    [Header(" Consts ")]
    private const string BUTTON = "Button";



    private void Awake()
    {
        transform.Find(BUTTON).GetComponent<Button>().onClick.AddListener(() =>
        {
            BuildingTypeSO buildingType = building.GetComponent<BuildingTypeHolder>().buildingType;

            foreach (ResourceAmount resourceAmount in buildingType.constructionResourceCostArray)
            {
                ResourceManager.Instance.AddResource(resourceAmount.resourceType, Mathf.FloorToInt(resourceAmount._amount * 0.6f));
            }

            Destroy(building.gameObject);
        });
    }
}
