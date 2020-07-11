using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float BeamForce = 5;
    public float BeamLength = 5;
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
            DrawLineTo(mousePosition);
        }
        if(Input.GetMouseButtonUp(0))
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
    void DrawLineTo(Vector3 endPosition)
    {
        endPosition.z = 0;
        var line = new GameObject();
        magnetBeam = line.AddComponent<LineRenderer>();
        magnetBeam.material = new Material(Shader.Find("Sprites/Default"));
        magnetBeam.startColor = Color.white;
        magnetBeam.endColor = Color.white;
        magnetBeam.startWidth = 0.4f;
        magnetBeam.endWidth = 0.4f;
        magnetBeam.SetPositions(new Vector3[] { transform.position, transform.position });
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
