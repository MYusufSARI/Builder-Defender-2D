using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header(" Elements ")]
    private Transform targetTransform;
    private Rigidbody2D rigidbody2D;


    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();

        targetTransform =  BuildingManager.Instance.GetHQBuilding().transform;
    }


    private void Update()
    {
        Vector3 moveDir = (targetTransform.position - transform.position).normalized;
        float moveSpeed = 6f;

        rigidbody2D.velocity = moveDir * moveSpeed;
    }

}
