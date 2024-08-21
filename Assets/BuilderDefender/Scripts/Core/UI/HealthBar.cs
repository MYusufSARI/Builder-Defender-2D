using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private HealthManager healthManager;
    private Transform barTransform;
    private Transform seperatorContainer;


    [Header(" Consts ")]
    private const string BAR = "Bar";
    private const string SEPERATOR_CONTAINER = "SeperatorContainer";
    private const string SEPERATOR_TEMPLATE = "SeperatorTemplate";



    private void Awake()
    {
        barTransform = transform.Find(BAR);
    }


    private void Start()
    {
        seperatorContainer = transform.Find(SEPERATOR_CONTAINER);

        ConstructHealthBarSeperators();

        healthManager.OnDamaged += OnDamagedCallback;
        healthManager.OnHealed += OnHealedCallback;
        healthManager.OnHealthAmountMaxChanged += HealthAmountMaxChangedCallback;

        UpdateBar();
        UpdateHealthBarVisible();
    }


    private void OnDestroy()
    {
        healthManager.OnDamaged -= OnDamagedCallback;
        healthManager.OnHealed -= OnHealedCallback;
        healthManager.OnHealthAmountMaxChanged -= HealthAmountMaxChangedCallback;
    }

    private void HealthAmountMaxChangedCallback(object sender, EventArgs e)
    {
        ConstructHealthBarSeperators();
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


    private void ConstructHealthBarSeperators()
    {
        Transform seperatorTemplate = seperatorContainer.Find(SEPERATOR_TEMPLATE);
        seperatorTemplate.gameObject.SetActive(false);

        foreach (Transform  seperatorTransform in seperatorContainer)
        {
            if (seperatorTransform == seperatorTemplate)
                continue;

            Destroy(seperatorTransform.gameObject);
        }

        int healtAmountPerSeperator = 10;
        float barSize = 3f;
        float barOneHealthAmountMax = barSize / healthManager.GetHealthAmountMax();

        int healthSeperatorCount = Mathf.FloorToInt(healthManager.GetHealthAmountMax() / healtAmountPerSeperator);

        for (int i = 1; i < healthSeperatorCount; i++)
        {
            Transform seperatorTransform = Instantiate(seperatorTemplate, seperatorContainer);

            seperatorTransform.gameObject.SetActive(true);
            seperatorTransform.localPosition = new Vector3(barOneHealthAmountMax * i * healtAmountPerSeperator, 0, 0);
        }
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

        gameObject.SetActive(true);
    }
}
