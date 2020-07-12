using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineCreator : MonoBehaviour
{
    public LineRenderer DrawLineTo(Vector3 direction, Material material, float length, float width)
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
}
