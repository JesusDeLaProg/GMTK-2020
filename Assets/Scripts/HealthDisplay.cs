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
        }
    }

    void Start()
    {
        Display.text = "Health : ";
    }
}