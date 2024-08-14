using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance { get; private set; }

    [Header(" Elements ")]
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
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (activeBuildingType != null && CanSpawnBuilding(activeBuildingType, UtilsClass.GetMouseWorldPosition()))
            {
                Instantiate(activeBuildingType._prefab, UtilsClass.GetMouseWorldPosition(), Quaternion.identity);
            }
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


    private bool CanSpawnBuilding(BuildingTypeSO buildingType, Vector3 position)
    {
        BoxCollider2D boxCollider2D = buildingType._prefab.GetComponent<BoxCollider2D>();

        Collider2D[] collider2DArray = Physics2D.OverlapBoxAll(position + (Vector3)boxCollider2D.offset, boxCollider2D.size, 0);

        foreach (Collider2D collider2D in collider2DArray)
        {
            // Colliders on top of the building position
        }

        return collider2DArray.Length == 0;
    }
}
