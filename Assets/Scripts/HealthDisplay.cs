﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{

    private int _health = 5;
    public Text Display;
    public Text Fuel;
    public Image[] Healths;
    public int health
    {
        get
        {
            return _health;
        }
        set
        {
           _health = value;
           StartCoroutine(LostLife(_health));
        }
    }

    IEnumerator LostLife(int index)
    {
        Healths[index].color =new Color32(255,0,0,255);
        yield return new WaitForSeconds(.2f);
        Healths[index].color =new Color32(255,0,0,0);
    }

    public IEnumerator FuelEmpty(){
        while(true){
            Fuel.color = new Color32(255,0,0,255);
            yield return new WaitForSeconds(1.5f);
            Fuel.color = new Color32(0,255,0,0);
            yield return new WaitForSeconds(1.5f);
        }
    }

    void Start()
    {        
        Display.text = "Health : ";
    }
}