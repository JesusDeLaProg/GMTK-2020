using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float BeamForce = 5;
    public float BeamLength = 5;
    public float BeamWidth = 0.1f;
    public Material BeamMaterial;

    public float LaserLength = 100;
    public float LaserWidth = 0.4f;
    public float LaserLifeSpan = 0.5f;
    public float LaserRecoil = 1;
    public Material LaserMaterial;
    
    public Transform Tip;
    public float Torque = 1;

    new Rigidbody2D rigidbody;

    private LineRenderer magnetBeam;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        if(Input.GetMouseButtonDown(0))
        {
            magnetBeam = DrawLineTo(mousePosition - transform.position, BeamMaterial, BeamLength, BeamWidth);
        }
        if (Input.GetMouseButtonDown(1))
        {
            if(GameManager.instance.LaserReady)
            {
                GameManager.instance.LaserReady = false;
                var laser = DrawLineTo(mousePosition - transform.position, LaserMaterial, LaserLength, LaserWidth);
                rigidbody.AddForce((transform.position - mousePosition).normalized * LaserRecoil, ForceMode2D.Impulse);
                Destroy(laser.gameObject, LaserLifeSpan);
                DestroyAsteroidsInDirectionOf((mousePosition - transform.position).normalized);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            Destroy(magnetBeam.gameObject);
            magnetBeam = null;
        }
        if(Input.GetMouseButton(0))
        {
            AttractInDirectionOf((mousePosition - transform.position).normalized);
        }
        if (magnetBeam)
        {
            UpdateLinePositions(mousePosition);
        }
    }

    // Create
    LineRenderer DrawLineTo(Vector3 direction, Material material, float length, float width)
    {
        direction.Normalize();
        var line = new GameObject();
        var lineRenderer = line.AddComponent<LineRenderer>();
        lineRenderer.material = material;
        lineRenderer.numCapVertices = 40;
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;
        lineRenderer.SetPositions(new Vector3[] { transform.position, transform.position + direction * length });
        return lineRenderer;
    }

    void UpdateLinePositions(Vector3 mousePosition)
    {
        magnetBeam.SetPositions(new Vector3[] { transform.position, transform.position + ((mousePosition - transform.position).normalized * BeamLength) });
    }

    void AttractInDirectionOf(Vector2 direction)
    {
        var thisPos = new Vector2(transform.position.x, transform.position.y);
        var hits = Physics2D.RaycastAll(thisPos, direction, BeamLength);
        foreach(var hit in hits.Where(h => h.rigidbody != null))
        {
            Vector2 force = direction * BeamForce;
            hit.rigidbody.AddForce(force * -1);
            rigidbody.AddForce(force);

        }
        Vector3 forward3 = (Tip.transform.position - transform.position);
        Vector2 forward = new Vector2(forward3.x, forward3.y).normalized;
        float angle = Vector2.SignedAngle(forward, direction);
        if(hits.Length > 0)
        {
            rigidbody.AddTorque(Torque * Mathf.Sign(angle));
        }
    }

    void DestroyAsteroidsInDirectionOf(Vector2 direction)
    {
        var thisPos = new Vector2(transform.position.x, transform.position.y);
        var hits = Physics2D.RaycastAll(thisPos, direction, BeamLength);
        foreach (var hit in hits.Where(h => h.transform.gameObject.CompareTag("Asteroid")))
        {
            var asteroid = hit.transform.gameObject.GetComponent<Asteroid>();
            asteroid.Split(direction);
        }
    }
}
