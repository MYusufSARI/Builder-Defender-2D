using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static Enemy Create(Vector3 position)
    {
        Transform pfEnemy = Resources.Load<Transform>("PF_Enemy");
        Transform enemyTransform =  Instantiate(pfEnemy, position, Quaternion.identity);;

        Enemy enemy = enemyTransform.GetComponent<Enemy>();

        return enemy;
    }


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


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Building building= collision.gameObject.GetComponent<Building>();

        if (building != null)
        {
            // Collided with a building
            HealthManager healthManager= building.GetComponent<HealthManager>();

            healthManager.Damage(10);
            Destroy(gameObject);
        }
    }
}
