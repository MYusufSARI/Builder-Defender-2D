using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance { get; private set; }

    [Header(" Elements ")]
    [SerializeField] private Building hqBuilding;
    private Camera mainCamera;
    private BuildingTypeListSO buildingTypeList;
    private BuildingTypeSO activeBuildingType;



    [Header(" Events ")]
    public static Action<OnActiveBuildingTypeChangedEvent> onActiveBuildingTypeChanged;

    public class OnActiveBuildingTypeChangedEvent
    {
        public BuildingTypeSO activeBuildingType;
    }



    private void Awake()
    {
        Instance = this;

        buildingTypeList = (Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name));
    }


    private void Start()
    {
        mainCamera = Camera.main;
    }


    private void Update()
    {
        ManageSpawning();
    }


    private void ManageSpawning()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (activeBuildingType != null)
            {
                if (CanSpawnBuilding(activeBuildingType, UtilsClass.GetMouseWorldPosition(), out string errorMessage))
                {
                    if (ResourceManager.Instance.CanAfford(activeBuildingType.constructionResourceCostArray))
                    {
                        ResourceManager.Instance.SpendResources(activeBuildingType.constructionResourceCostArray);
                        Instantiate(activeBuildingType._prefab, UtilsClass.GetMouseWorldPosition(), Quaternion.identity);
                    }

                    else
                    {
                        ToolTipUI.Instance.Show("Cannot afford " + activeBuildingType.GetConstructşonResourceCostString(),
                            new ToolTipUI.TooltipTimer { _timer = 2f });
                    }
                }

                else
                {
                    ToolTipUI.Instance.Show(errorMessage, new ToolTipUI.TooltipTimer { _timer = 2f });
                }

            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 enemySpawnPosition = UtilsClass.GetMouseWorldPosition() + UtilsClass.GetRandomDirection() * 5f;

            Enemy.Create(enemySpawnPosition);
        }
    }


    public void SetActiveBuildingType(BuildingTypeSO buildingType)
    {
        activeBuildingType = buildingType;

        onActiveBuildingTypeChanged?.Invoke(
            new OnActiveBuildingTypeChangedEvent { activeBuildingType = activeBuildingType }
            );
    }


    public BuildingTypeSO GetActiveBuildingType()
    {
        return activeBuildingType;
    }


    private bool CanSpawnBuilding(BuildingTypeSO buildingType, Vector3 position, out string errorMessage)
    {
        BoxCollider2D boxCollider2D = buildingType._prefab.GetComponent<BoxCollider2D>();

        Collider2D[] collider2DArray = Physics2D.OverlapBoxAll(position + (Vector3)boxCollider2D.offset, boxCollider2D.size, 0);

        bool isAreClear = collider2DArray.Length == 0;

        if (!isAreClear)
        {
            errorMessage = "Area is not clear!";
            return false;
        }

        collider2DArray = Physics2D.OverlapCircleAll(position, buildingType.minConstructionRadius);

        foreach (Collider2D collider2D in collider2DArray)
        {
            // Colliders inside the construction radius
            BuildingTypeHolder buildingTypeHolder = collider2D.GetComponent<BuildingTypeHolder>();

            if (buildingTypeHolder != null)
            {
                if (buildingTypeHolder.buildingType == buildingType)
                {
                    // There is already a building of this type within the construction radius!
                    errorMessage = "Too close to another building of the same type!";
                    return false;
                }
            }
        }


        collider2DArray = Physics2D.OverlapCircleAll(position, buildingType.maxConstructionRadius);

        foreach (Collider2D collider2D in collider2DArray)
        {
            // Colliders inside the construction radius
            BuildingTypeHolder buildingTypeHolder = collider2D.GetComponent<BuildingTypeHolder>();

            if (buildingTypeHolder != null)
            {
                // It's a building!
                errorMessage = "";
                return true;
            }
        }
        errorMessage = "Too far from any other building!";
        return false;
    }


    public Building GetHQBuilding()
    {
        return hqBuilding;
    }
}
