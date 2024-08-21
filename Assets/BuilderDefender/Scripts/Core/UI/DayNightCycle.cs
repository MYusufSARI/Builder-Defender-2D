using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayNightCycle : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Gradient _gradient;
    private Light2D _light2D;


    [Header(" Settings ")]
    [SerializeField] private float secondsPerDay;
    private float dayTime;
    private float dayTimeSpeed;


    private void Awake()
    {
        _light2D = GetComponent<Light2D>();

        dayTimeSpeed = 1 / secondsPerDay;
    }


    private void Update()
    {
        dayTime += Time.deltaTime * dayTimeSpeed;

        _light2D.color = _gradient.Evaluate(dayTime % 1f);
    }
}
