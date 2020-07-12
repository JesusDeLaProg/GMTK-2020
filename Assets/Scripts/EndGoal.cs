using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGoal : MonoBehaviour
{
    GameManager GM;
    private void Start() 
    {
        GM = FindObjectOfType<GameManager>();
    }
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Spaceship")
        {
            GM.EndLevel();
        }
    }
}
