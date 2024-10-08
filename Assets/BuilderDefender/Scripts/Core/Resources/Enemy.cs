using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static Enemy Create(Vector3 position)
    {
        Transform pfEnemy = Resources.Load<Transform>("PF_Enemy");
        Transform enemyTransform = Instantiate(pfEnemy, position, Quaternion.identity); ;

        Enemy enemy = enemyTransform.GetComponent<Enemy>();

        return enemy;
    }


    [Header(" Elements ")]
    private Transform targetTransform;
    private Rigidbody2D rigidbody2D;


    [Header(" Data ")]
    private HealthManager healthManager;


    [Header(" Settings ")]
    private float lookForTargetTimer;
    private float lookForTargetTimerMax = 0.2f;



    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();

        healthManager = GetComponent<HealthManager>();

        if (BuildingManager.Instance.GetHQBuilding() != null)
        {
            targetTransform = BuildingManager.Instance.GetHQBuilding().transform;
        }

        healthManager.OnDamaged += OnDamagedCallback;
        healthManager.OnDied += OnDiedCallback;

        lookForTargetTimer = Random.Range(0f, lookForTargetTimerMax);
    }


    private void OnDestroy()
    {
        healthManager.OnDamaged -= OnDamagedCallback;
        healthManager.OnDied -= OnDiedCallback;
    }


    private void OnDamagedCallback(object sender, System.EventArgs e)
    {
        CinemachineShake.Instance.ShakeCamera(4f, 0.07f);
        ChromaticAberrationEffect.Instance.SetWeight(0.5f);
    }


    private void OnDiedCallback(object sender, System.EventArgs e)
    {
        CinemachineShake.Instance.ShakeCamera(7f, 0.15f);

        ChromaticAberrationEffect.Instance.SetWeight(0.5f);

        Instantiate(Resources.Load<Transform>("PF_EnemyDieParticles"), transform.position, Quaternion.identity);

        Destroy(gameObject);
    }


    private void Update()
    {
        HandleMovement();

        HandleTargeting();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Building building = collision.gameObject.GetComponent<Building>();

        if (building != null)
        {
            // Collided with a building
            HealthManager healthManager = building.GetComponent<HealthManager>();

            healthManager.Damage(10);

            this.healthManager.Damage(999);
        }
    }


    private void HandleMovement()
    {
        if (targetTransform != null)
        {
            Vector3 moveDir = (targetTransform.position - transform.position).normalized;

            float moveSpeed = 6f;
            rigidbody2D.velocity = moveDir * moveSpeed;
        }

        else
        {
            rigidbody2D.velocity = Vector2.zero;
        }
    }


    private void HandleTargeting()
    {
        lookForTargetTimer -= Time.deltaTime;

        if (lookForTargetTimer < 0f)
        {
            lookForTargetTimer += lookForTargetTimerMax;

            LookForTargets();
        }
    }


    private void LookForTargets()
    {
        float targetMaxRadius = 10f;

        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, targetMaxRadius);

        foreach (Collider2D collider2D in collider2DArray)
        {
            Building building = collider2D.GetComponent<Building>();

            if (building != null)
            {
                // It's a building!
                if (targetTransform == null)
                {
                    targetTransform = building.transform;
                }
                else
                {
                    if (Vector3.Distance(transform.position, building.transform.position) <
                        Vector3.Distance(transform.position, targetTransform.position))
                    {
                        // Closer!
                        targetTransform = building.transform;
                    }
                }
            }
        }

        if (targetTransform == null)
        {
            // Found no target within range
            if (BuildingManager.Instance.GetHQBuilding() != null)
            {
                targetTransform = BuildingManager.Instance.GetHQBuilding().transform;
            }
        }
    }
}
