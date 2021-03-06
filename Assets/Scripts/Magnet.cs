﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    public float BeamForce = 10;
    public float BeamLength = 7;
    public float BeamWidth = 0.1f;
    public Material BeamMaterial;

    public GameObject Tip;

    public float Torque = 1;

    private new Rigidbody2D rigidbody;
    private LineCreator lineCreator;
    private LineRenderer beam;
    private AudioSourceManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        lineCreator = GetComponent<LineCreator>();
        audioManager = GetComponent<AudioSourceManager>();
    }

    public void Activate()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        beam = lineCreator.DrawLineTo(mousePosition - transform.position, BeamMaterial, BeamLength, BeamWidth);
        audioManager.PlayMagnet();
    }

    public void Deactivate()
    {
        Destroy(beam?.gameObject);
        beam = null;
        audioManager.StopMagnet();
    }

    // Update is called once per frame
    void Update()
    {
        if(beam)
        {
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            var beamDirection = (mousePosition - transform.position).normalized;
            beam.SetPositions(new Vector3[] { transform.position, transform.position + beamDirection * BeamLength });
            AttractInDirectionOf(mousePosition - transform.position);
        }
    }

    private void AttractInDirectionOf(Vector2 direction)
    {
        direction.Normalize();
        var thisPos = new Vector2(transform.position.x, transform.position.y);
        var hits = Physics2D.RaycastAll(thisPos, direction, BeamLength);
        foreach (var hit in hits.Where(h => h.rigidbody != null))
        {
            Vector2 force = direction * BeamForce;
            hit.rigidbody.AddForce(force * -1);
            rigidbody.AddForce(force);

        }
        Vector3 forward3 = Tip.transform.position - transform.position;
        Vector2 forward = new Vector2(forward3.x, forward3.y).normalized;
        float angle = Vector2.SignedAngle(forward, direction);
        if (hits.Length > 0)
        {
            audioManager.UnmuteMagnet();
            rigidbody.AddTorque(Torque * Mathf.Sign(angle));
        }
        else
        {
            audioManager.MuteMagnet();
        }
    }
}
