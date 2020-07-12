using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSystem : MonoBehaviour
{
    AudioSource audioSource;
    float defaultValue;



    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        defaultValue = audioSource.volume;
    }

    public void RestartSong()
    {
        audioSource.Stop();
        audioSource.Play();
    }

    public void LowerVolume()
    {
        StartCoroutine(VolumeFade(audioSource, ((defaultValue*25)*0.01f),0.50f));
    }

    public void BackToNormalVolume()
    {
        StartCoroutine(VolumeFade(audioSource, defaultValue ,0.50f));
    }

    IEnumerator VolumeFade(AudioSource _AudioSource, float _EndVolume, float _FadeLength)
    {

        float _StartVolume = _AudioSource.volume;

        float _StartTime = Time.time;

        while (Time.time < _StartTime + _FadeLength)
        {

            _AudioSource.volume = _StartVolume + ((_EndVolume - _StartVolume) * ((Time.time - _StartTime) / _FadeLength));

            yield return null;

        }

        if (_EndVolume == 0) { _AudioSource.Stop(); }

    }
}
