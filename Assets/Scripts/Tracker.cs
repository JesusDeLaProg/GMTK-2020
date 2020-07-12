using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tracker : MonoBehaviour
{
    private Transform _target;
    private Transform _ship;
    private SpriteRenderer sr;

    private void findTargets()
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
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_target || !_ship) findTargets();

        if(_target && _ship)
        {
            var worldDirection = (_target.position - _ship.position).normalized;
       
            transform.position = _ship.position + worldDirection *2;
            transform.rotation = Quaternion.LookRotation(transform.forward, worldDirection);
        }
    }

    public void Show()
    {
        sr.color = new Color(255, 0, 0, 255);
    }

    public void Hide()
    {
        sr.color = new Color(255, 0, 0, 0);
    }
}
