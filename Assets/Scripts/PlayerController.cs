using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private LaserCanon laserCanon;
    private Magnet magnet;
    private SpriteRenderer ship;
    private bool hurting;

    // Start is called before the first frame update
    void Start()
    {
        ship = GetComponent<SpriteRenderer>();
        laserCanon = GetComponent<LaserCanon>();
        magnet = GetComponent<Magnet>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        if(Input.GetMouseButtonDown(0))
        {
            magnet.Activate();
        }
        if (Input.GetMouseButtonDown(1))
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
        if(collision.gameObject.CompareTag("Asteroid"))
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
}
