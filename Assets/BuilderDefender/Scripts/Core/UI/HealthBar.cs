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
        healthManager.OnHealed += OnHealedCallback;

        UpdateBar();
        UpdateHealthBarVisible();
    }


    private void OnDestroy()
    {
        healthManager.OnDamaged -= OnDamagedCallback;
        healthManager.OnHealed -= OnHealedCallback;
    }


    private void OnHealedCallback(object sender, EventArgs e)
    {
        UpdateBar();
        UpdateHealthBarVisible();
    }


    private void OnDamagedCallback(object sender, EventArgs e)
    {
        UpdateBar();
        UpdateHealthBarVisible();
    }


    private void UpdateBar()
    {
        barTransform.localScale = new Vector3(healthManager.GetHealthAmountNormalized(), 1, 1);
    }


    private void UpdateHealthBarVisible()
    {
        if (healthManager.IsFullHealth())
        {
            gameObject.SetActive(false);
        }

        else
        {
            gameObject.SetActive(true);
        }
    }
}
