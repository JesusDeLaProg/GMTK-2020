using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{

    private int _health = 5;
    public Text Display;
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
           for(int i = value; i < Healths.Length; i++){
               Healths[i].color =new Color32(255,255,255,0);;
            }
        }
    }

    void Start()
    {        
        Display.text = "Health : ";
    }
}