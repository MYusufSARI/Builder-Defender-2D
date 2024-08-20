using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingRepairButton : MonoBehaviour
{
    [Header(" Data ")]
    [SerializeField] private HealthManager healthManager;
    [SerializeField] private ResourceTypeSO goldResourceType;


    [Header(" Consts ")]
    private const string BUTTON = "Button";



    private void Awake()
    {
        ManageRepairCost();
    }


    private void ManageRepairCost()
    {
        transform.Find(BUTTON).GetComponent<Button>().onClick.AddListener(() =>
        {
            int missingHealth = healthManager.GetHealthAmountMax() - healthManager.GetHealthAmount();
            int repairCost = missingHealth / 2;

            ResourceAmount[] resourceAmountCost = new ResourceAmount[]
            {
                new ResourceAmount{resourceType = goldResourceType, _amount = repairCost}
            };

            if (ResourceManager.Instance.CanAfford(resourceAmountCost))
            {
                // Can afford the repairs
                ResourceManager.Instance.SpendResources(resourceAmountCost);
                healthManager.HealFull();
            }

            else
            {
                // Cannot affor the repairs
                ToolTipUI.Instance.Show("Cannot afford repair cost!", new ToolTipUI.TooltipTimer { _timer = 2f });
            }
        });
    }
}
