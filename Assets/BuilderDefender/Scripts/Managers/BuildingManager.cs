using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Transform mouseVisualTransform;
    private Camera mainCamera;



    private void Start()
    {
        mainCamera = Camera.main;
    }


    private void Update()
    {
        mouseVisualTransform.position = GetMouseWorldPosition();
    }


    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        mouseWorldPosition.z = 0f;

        return mouseWorldPosition;
    }
}
