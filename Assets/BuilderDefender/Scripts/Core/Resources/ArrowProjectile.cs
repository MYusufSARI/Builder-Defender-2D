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


    [Header(" Settings ")]
    private Vector3 lastMoveDir;
    private float timeToDie =2f;



    private void Update()
    {
        Vector3 moveDir; 
        if (targetEnemy != null)
        {
            moveDir = (targetEnemy.transform.position - transform.position).normalized;
            lastMoveDir = moveDir;
        }

        else
        {
            moveDir = lastMoveDir;
        }

        float moveSpeed = 20f;

        transform.position += lastMoveDir * moveSpeed * Time.deltaTime;

        // Rotate the arrows towards to the enemys position
        transform.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVector(moveDir));

        timeToDie -= Time.deltaTime;

        if (timeToDie < 0f)
        {
            Destroy(gameObject);
        }
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
