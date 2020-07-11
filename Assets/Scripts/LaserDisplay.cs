using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaserDisplay : MonoBehaviour
{
    private bool _laser = true;
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
            _laser = value;
        }
    }

    void Start()
    {
        Display.text = "Laser : ";
    }
}
