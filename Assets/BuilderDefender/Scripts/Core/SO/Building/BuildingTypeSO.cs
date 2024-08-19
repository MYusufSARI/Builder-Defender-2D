using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/BuildingType")]
public class BuildingTypeSO : ScriptableObject
{
    public string nameString;

    public Transform _prefab;

    public bool hasResourceGeneratorData;

    public ResourceGeneratorData resourceGeneratorData;

    public Sprite _sprite;

    public float minConstructionRadius;

    public float maxConstructionRadius;

    public ResourceAmount[] constructionResourceCostArray;

    public int healthAmountMax;

    public float constructionTimerMax;




    public string GetConstructşonResourceCostString()
    {
        string str = "";

        foreach (ResourceAmount resourceAmount in constructionResourceCostArray)
        {
            str += "<color=#" + resourceAmount.resourceType.colorHex + ">" +
                resourceAmount.resourceType.nameShort + resourceAmount._amount +
                "</color> ";
        }

        return str;
    }
}
