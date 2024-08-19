using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingDemolishButton : MonoBehaviour
{
    [Header(" Data ")]
    [SerializeField] private Building building ;


    [Header(" Consts ")]
    private const string BUTTON = "Button";



    private void Awake()
    {
        transform.Find(BUTTON).GetComponent<Button>().onClick.AddListener(() =>
        {
            Destroy(building.gameObject);
        });      
    }
}
