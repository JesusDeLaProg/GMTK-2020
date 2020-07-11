using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaserDisplay : MonoBehaviour
{
    private bool _laser = true;
    public float waitTime = 5.0f;
    public Text Display;
    public Image Lazer;
    public bool laser
    {
        get
        {
            return _laser;
        }
        set
        {
            if(_laser){
                _laser = value;
                Lazer.fillAmount = 0;      
                Lazer.color =  new Color32(255,255,255,255);
                StartCoroutine(Fade());
            }else{
                _laser = value;
            }
        }
    }

    IEnumerator Fade()
    {
        while(Lazer.fillAmount < 1)
        {
            Lazer.fillAmount += 1.0f / waitTime;
            yield return new WaitForSeconds(.1f);
        }
        Lazer.color =  new Color32(255,11,222,255);
        _laser = true;
    }
    void Start()
    {
        Display.text = "Laser : ";
    }
}
