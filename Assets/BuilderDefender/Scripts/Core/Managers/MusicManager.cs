using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [Header(" Elements ")]
    private AudioSource audioSource;


    [Header(" Settings ")]
    private float _volume = 0.5f;



    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.volume = _volume;
    }


    public void IncreaseVolume()
    {
        _volume += 0.1f;

        _volume = Mathf.Clamp01(_volume);
        audioSource.volume = _volume;
    }


    public void DecreaseVolume()
    {
        _volume -= 0.1f;

        _volume = Mathf.Clamp01(_volume);
        audioSource.volume = _volume;
    }


    public float GetVolume()
    {
        return _volume;
    } 
}
