using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool active;
    public bool Active
    {
        get { return active; }
        set
        {
            active = value;
            if (active) rigidbody.simulated = true;
        }
    }

    private LaserCanon laserCanon;
    private Magnet magnet;
    private SpriteRenderer ship;
    public Sprite deadShip;
    private bool hurting;

    private new Rigidbody2D rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        ship = GetComponent<SpriteRenderer>();
        laserCanon = GetComponent<LaserCanon>();
        magnet = GetComponent<Magnet>();
        Active = false;
    }

    public void setSpriteDeadShip()
    {
        ship.sprite = deadShip;
        ship.color = Color.red;
        ship.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        GetComponentInChildren<Tracker>().Hide();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        if(Input.GetMouseButtonDown(0) && Active)
        {
            magnet.Activate();
        }
        if (Input.GetMouseButtonDown(1) && Active)
        {
            if(GameManager.instance.LaserReady)
            {
                GameManager.instance.LaserReady = false;
                laserCanon.Shoot();
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            magnet.Deactivate();
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(Active && collision.gameObject.CompareTag("Asteroid"))
        {
            StartCoroutine(Hurt());
        }
    }

    private IEnumerator Hurt()
    {
        if(!hurting)
        {
            ship.color = Color.red;
            GameManager.instance.HitPoints--;
            hurting = true;
            yield return new WaitForSeconds(1f);
            ship.color = Color.white;
            hurting = false;
        }
    }

    public IEnumerator Stop(float duration)
    {
        Active = false;
        var start = DateTime.Now;
        while (rigidbody.velocity.magnitude > 0)
        {
            rigidbody.velocity *= (float)(1 - (DateTime.Now - start).TotalSeconds / duration);
            yield return new WaitForFixedUpdate();
        }
        rigidbody.velocity = Vector2.zero;
        rigidbody.simulated = false;
    }
}
