using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public GameObject SmallerAsteroid;
    public Asteroids Size;
    public enum Asteroids{Small = 0, Medium, Large, XLarge}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            var otherAsteroid = collision.gameObject.GetComponent<Asteroid>();
            if (Size <= otherAsteroid.Size)
            {
                Split(collision.contacts[0].normal);
            }
        }
    }
    public void Split(Vector2 Impact)
    {
        var perpendiculaire = Vector2.Perpendicular(Impact).normalized;
        var SplitAxe = new Vector3(perpendiculaire.x,perpendiculaire.y);
        Instantiate(SmallerAsteroid,transform.position + SplitAxe * transform.localScale.y,transform.rotation);
        Instantiate(SmallerAsteroid, transform.position - SplitAxe * transform.localScale.y, transform.rotation);
        Destroy(gameObject);
    }


}
