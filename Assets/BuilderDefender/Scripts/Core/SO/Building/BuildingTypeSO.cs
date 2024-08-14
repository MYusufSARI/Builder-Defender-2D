using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/BuildingType")]
public class BuildingTypeSO : ScriptableObject
{
    public string nameString;

    public Transform _prefab;

    public ResourceGeneratorData resourceGeneratorData;

    public Sprite _sprite;

    public float minConstructionRadius;
}
