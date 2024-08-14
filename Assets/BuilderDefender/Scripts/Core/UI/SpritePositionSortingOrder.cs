using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritePositionSortingOrder : MonoBehaviour
{
    [Header(" Elements ")]
    private SpriteRenderer spriteRenderer;


    [Header(" Settings ")]
    [SerializeField] private bool runOnce;



    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    private void LateUpdate()
    {
        float precisionMultiplier = 5f;
        spriteRenderer.sortingOrder = (int)(-transform.position.y * precisionMultiplier);

        if (runOnce)
        {
            Destroy(this);
        }
    }
}
