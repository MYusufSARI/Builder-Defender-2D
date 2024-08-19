  using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingConstruction : MonoBehaviour
{
    public static BuildingConstruction Create(Vector3 position, BuildingTypeSO buildingType)
    {
        Transform pfBuildingConstruction = Resources.Load<Transform>("PF_BuildingConstruction");
        Transform buildingConstructionTransform = Instantiate(pfBuildingConstruction, position, Quaternion.identity);

        BuildingConstruction buildingConstruction = buildingConstructionTransform.GetComponent<BuildingConstruction>();
        buildingConstruction.SetBuildingType(buildingType);

        return buildingConstruction;
    }


    [Header(" Elements ")]
    private BoxCollider2D boxCollider2D;
    private SpriteRenderer spriteRenderer;
    private Material constructionMaterial;  


    [Header(" Settings ")]
    private float constructionTimer;
    private float constructionTimerMax;


    [Header(" Data ")]
    private BuildingTypeSO buildingType;
    private BuildingTypeHolder buildingTypeHolder;


    [Header(" Consts ")]
    private const string SPRITE = "Sprite";
    private const string PROGRESS = "_Progress";



    private void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();

        spriteRenderer = transform.Find(SPRITE).GetComponent<SpriteRenderer>();

        buildingTypeHolder = GetComponent<BuildingTypeHolder>();

        constructionMaterial = spriteRenderer.material;
    }


    private void Update()
    {
        constructionTimer -= Time.deltaTime;

        constructionMaterial.SetFloat(PROGRESS, GetConstructionTimerNormalize());

        if (constructionTimer <= 0f)
        {
            Debug.Log("Ding!");
            Instantiate(buildingType._prefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }


    private void SetBuildingType(BuildingTypeSO buildingType)
    {
        this.buildingType = buildingType;



        constructionTimerMax = buildingType.constructionTimerMax;
        constructionTimer = constructionTimerMax;

        spriteRenderer.sprite = buildingType._sprite;

        boxCollider2D.offset = buildingType._prefab.GetComponent<BoxCollider2D>().offset;
        boxCollider2D.size = buildingType._prefab.GetComponent<BoxCollider2D>().size;

        buildingTypeHolder.buildingType = buildingType;
    }


    public float GetConstructionTimerNormalize()
    {
        return 1 - constructionTimer / constructionTimerMax;
    }

}
