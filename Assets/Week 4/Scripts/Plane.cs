using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Plane : MonoBehaviour
{
    public List<Vector2> points;
    public float newPointThreshold = 0.2f;
    Vector2 lastPos;
    LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, transform.position);
    }

    void OnMouseDown()
    {
        points = new List<Vector2>();
        Vector2 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        points.Add(newPos);
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, transform.position);
    }

    void OnMouseDrag()
    {
        Vector2 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Vector2.Distance(lastPos, newPos) > newPointThreshold)
        {    
            points.Add(newPos);
            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount-1, newPos);
            lastPos = newPos;
        }
    }
}
