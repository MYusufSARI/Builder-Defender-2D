using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [Header(" Elements ")]
    private HealthManager healthManager;


    private void Start()
    {
        healthManager = GetComponent<HealthManager>();

        healthManager.OnDied += OnDiedCallback;
    }

    private void OnDestroy()
    {
        healthManager.OnDied -= OnDiedCallback;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            healthManager.Damage(10);
        }
    }


    private void OnDiedCallback(object sender, EventArgs e)
    {
        Destroy(gameObject);
    }
}
