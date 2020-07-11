using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public HealthDisplay Health;
    public LaserDisplay Laser;
    public static GameManager instance {get;private set;}
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        GameManager.instance = this;
        StartCoroutine(Health.FuelEmpty());
        StartLevel();
    }

    public void StartLevel(){
        SceneManager.LoadScene("DefaultMap");
    }
    
    public void SetUI(){
        if(Input.GetKeyDown("space")){
           Health.health--;   
        }
    }

    public void EndLevel(){

    }

    public void EndGame(){

    }

    public void Update(){
        
        if(Input.GetKeyDown("space")){
           Health.health--;   
        }
        if(Input.GetKeyDown("enter")){
           Laser.laser = false;   
        }
    }
}
