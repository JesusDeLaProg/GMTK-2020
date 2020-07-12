using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LaserCanon : MonoBehaviour
{
    public float LaserLength = 100;
    public float LaserWidth = 0.4f;
    public float LaserLifeSpan = 0.1f;
    public float LaserRecoil = 300;
    public Material LaserMaterial;

    private LineCreator lineCreator;
    private new Rigidbody2D rigidbody;

    public void Start()
    {
        lineCreator = GetComponent<LineCreator>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Shoot()
    {
        var laser = lineCreator.DrawLineTo(transform.up, LaserMaterial, LaserLength, LaserWidth);
        Destroy(laser, LaserLifeSpan);
        rigidbody.AddForce(transform.up * -1 * LaserRecoil, ForceMode2D.Impulse);
        DestroyAsteroidsInDirectionOf(transform.up);
        gameObject.GetComponent<AudioSourceManager>().PlayLaser();
    }

    private void DestroyAsteroidsInDirectionOf(Vector2 direction)
    {
        var thisPos = new Vector2(transform.position.x, transform.position.y);
        var hits = Physics2D.RaycastAll(thisPos, direction, LaserLength);
        foreach (var hit in hits.Where(h => h.transform.gameObject.CompareTag("Asteroid")))
        {
            var asteroid = hit.transform.gameObject.GetComponent<Asteroid>();
            asteroid.Split(direction);
        }
    }
}
