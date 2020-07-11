using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public HealthDisplay Health;
    public static GameManager instance {get;private set;}
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        GameManager.instance = this;
    }

    public void StartLevel(){

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
    }
}
