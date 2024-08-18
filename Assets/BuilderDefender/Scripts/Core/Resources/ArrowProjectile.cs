using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowProjectile : MonoBehaviour
{
    public static ArrowProjectile Create(Vector3 position, Enemy enemy)
    {
        Transform pfArrowPrjectile = Resources.Load<Transform>("PF_ArrowProjectile");
        Transform arrowTransform = Instantiate(pfArrowPrjectile, position, Quaternion.identity);

        ArrowProjectile arrowProjectile = arrowTransform.GetComponent<ArrowProjectile>();
        arrowProjectile.SetTarget(enemy);

        return arrowProjectile;
    }


    [Header(" Elements ")]
    private Enemy targetEnemy;



    private void Update()
    {
        Vector3 moveDir = (targetEnemy.transform.position - transform.position).normalized;

        float moveSpeed = 20f;

        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }


    private void SetTarget(Enemy targetEnemy)
    {
        this.targetEnemy = targetEnemy;
    } 


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();

        if (enemy != null)
        {
            // Hit an enemy!
            Destroy(gameObject);
        }
    }
}
