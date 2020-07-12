﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public HealthDisplay Health;
    public LaserDisplay Laser;
    public AnimController dialogue;

    public bool LaserReady{
        get{
            return Laser.laser;
        }
        set{
            Laser.laser = value;
        }
    }

    public int HitPoints{
        get{
            return Health.health;
        }
        set{
            Health.health = value;
            if(value == 0){
                EndGame();
            }
        }
    }

    public static GameManager instance {get;private set;}
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);        
        GameManager.instance = this;
        StartCoroutine(Health.FuelEmpty());
        dialogue = GameObject.FindObjectOfType<AnimController>();
        dialogue.StartAnim = true;
        StartLevel();
    }

    public void StartLevel(){
        DontDestroyOnLoad(GameObject.FindGameObjectWithTag("Canvas"));
        SceneManager.LoadScene("DefaultMap");
    }
    
    public void SetUI(){
        if(Input.GetKeyDown("space")){
           Health.health--;   
        }
    }

    public void EndLevel()
    {
        Debug.Log("Bravo !");
    }

    public void EndGame()
    {
        Debug.Log("RIP !");
    }

    public void Update(){
        
    }
}
