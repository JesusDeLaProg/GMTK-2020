using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceManager : MonoBehaviour
{
    public AudioSource LaserSound;
    public AudioSource MagnetSound;

    public void PlayMagnet()
    {
        MagnetSound.volume = 1;
        MagnetSound.Play();
    }

    public void StopMagnet()
    {
        StartCoroutine(VolumeFade(MagnetSound, 0, 0.1f));
    }

    public void PlayLaser()
    {
        LaserSound.Play();
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
 
         if (_EndVolume == 0) {_AudioSource.Stop();}
         
     }
}
