using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    [Header(" Settings ")]
    private float _timer;
    private float timerMax;



    private void Awake()
    {
        timerMax = 1f;
    }


    private void Update()
    {
        _timer -= Time.deltaTime;

        if (_timer <= 0f)
        {
            _timer += timerMax;

            Debug.Log("Ding!");
        }
    }

}
