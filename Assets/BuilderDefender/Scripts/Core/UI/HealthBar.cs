using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private HealthManager healthManager;
    private Transform barTransform;


    [Header(" Consts ")]
    private const string BAR = "Bar";



    private void Awake()
    {
        barTransform = transform.Find(BAR);
    }


    private void Start()
    {
        healthManager.OnDamaged += OnDamagedCallback;

        UpdateBar();
    }


    private void OnDestroy()
    {
        healthManager.OnDamaged -= OnDamagedCallback;
    }


    private void OnDamagedCallback(object sender, EventArgs e)
    {
        UpdateBar();
    }


    private void UpdateBar()
    {
        barTransform.localScale = new Vector3(healthManager.GetHealthAmountNormalized(), 1, 1);
    }
}
