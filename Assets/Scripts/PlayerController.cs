using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float BeamForce = 5;
    public float BeamLength = 5;
    public Material BeamMaterial;
    public float LaserLength = 100;
    public float LaserLifeSpan = 0.5f;
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
            magnetBeam = DrawLineTo(mousePosition - transform.position, BeamMaterial, BeamLength);
        }
        if (Input.GetMouseButtonDown(1))
        {

            var laser = DrawLineTo(mousePosition - transform.position, LaserMaterial, LaserLength);
            Destroy(laser.gameObject, LaserLifeSpan);
        }
        if (Input.GetMouseButtonUp(0))
        {
            Destroy(magnetBeam.gameObject);
            magnetBeam = null;
        }
        if(Input.GetMouseButton(0))
        {
            AttractInDirectionOf(mousePosition);
        }
        if (magnetBeam)
        {
            UpdateLinePositions(mousePosition);
        }
    }

    // Create
    LineRenderer DrawLineTo(Vector3 direction, Material material, float length)
    {
        direction.Normalize();
        var line = new GameObject();
        var lineRenderer = line.AddComponent<LineRenderer>();
        lineRenderer.material = material;
        lineRenderer.numCapVertices = 40;
        lineRenderer.startWidth = 0.4f;
        lineRenderer.endWidth = 0.4f;
        lineRenderer.SetPositions(new Vector3[] { transform.position, transform.position + direction * length });
        return lineRenderer;
    }

    void UpdateLinePositions(Vector3 mousePosition)
    {
        magnetBeam.SetPositions(new Vector3[] { transform.position, transform.position + ((mousePosition - transform.position).normalized * BeamLength) });
    }

    void AttractInDirectionOf(Vector2 position)
    {
        var thisPos = new Vector2(transform.position.x, transform.position.y);
        var hits = Physics2D.RaycastAll(thisPos, (position - thisPos).normalized, BeamLength);
        foreach(var hit in hits)
        {
            Vector3 direction = hit.transform.position - transform.position;
            Vector2 force = new Vector2(direction.x, direction.y).normalized * BeamForce;
            hit.rigidbody.AddForce(force * -1);
            rigidbody.AddForce(force);

        }
        Vector3 forward3 = (Tip.transform.position - transform.position);
        Vector2 forward = new Vector2(forward3.x, forward3.y).normalized;
        float angle = Vector2.SignedAngle(forward, (position - thisPos).normalized);
        if(hits.Length > 0)
        {
            rigidbody.AddTorque(Torque * Mathf.Sign(angle));
        }
    }
}
