using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGhost : MonoBehaviour
{
    [Header(" Elements ")]
    private GameObject spriteGameObject;



    [Header(" Consts ")]
    private const string SPRİTE = "Sprite";


    private void Awake()
    {
        spriteGameObject = transform.Find(SPRİTE).gameObject;

        Hide();
    }


    private void Update()
    {
        
    }


    private void Show(Sprite ghostSprite)
    {
        spriteGameObject.SetActive(true);

        spriteGameObject.GetComponent<SpriteRenderer>().sprite = ghostSprite;
    }


    private void Hide()
    {
        spriteGameObject.SetActive(false);
    }
}
