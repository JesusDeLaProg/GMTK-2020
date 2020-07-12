using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tracker : MonoBehaviour
{
    public Image img;
    private Transform _target;
    private Transform _ship;

    void findTargets(Scene scene, LoadSceneMode mode)
    {
        var finish = GameObject.FindGameObjectWithTag("Finish");
        var spaceship = GameObject.FindGameObjectWithTag("Spaceship");
        if (finish && spaceship)
        {
            _target = finish.transform;
            _ship = spaceship.transform;
        }
    }

    private void Start()
    {
        SceneManager.sceneLoaded += findTargets; 
    }

    // Update is called once per frame
    void Update()
    {
        if(_target && _ship)
        {
            var worldPosition = _ship.position + (_target.position - _ship.position).normalized * 2;
            var screenPosition = Camera.main.WorldToScreenPoint(worldPosition);

            img.transform.position = screenPosition;



        }
    }    
}
