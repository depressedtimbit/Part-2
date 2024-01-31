using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Plane : MonoBehaviour
{
    public List<Vector2> points;
    public float newPointThreshold = 0.2f;
    public float speed = 1;
    public AnimationCurve landingCurve;
    Vector2 lastPos;
    LineRenderer lineRenderer;
    Rigidbody2D rb;
    Vector2 currentPos;
    float landingTimer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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

    void FixedUpdate()
    {
        currentPos = transform.position;
        if(points.Count > 0)
        {
            Vector2 direction = points[0] - currentPos;
            float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            rb.rotation = -angle;
        }
        rb.MovePosition(rb.position + (Vector2)transform.up * speed * Time.deltaTime);
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            landingTimer += 0.5f * Time.deltaTime;
            float interpolation = landingCurve.Evaluate(landingTimer);
            if(transform.localScale.z < 0.1f)
            {
                Destroy(gameObject);
            }
            transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, interpolation);
        }
        

        lineRenderer.SetPosition(0, transform.position);
        if(points.Count > 0)
        {
           if(Vector2.Distance(currentPos, points[0]) < newPointThreshold)
           {
                points.RemoveAt(0);
                
                for(int i = 0; i < lineRenderer.positionCount - 2; i++) 
                {
                    lineRenderer.SetPosition(i, lineRenderer.GetPosition(i + 1));
                }
                lineRenderer.positionCount--;
           }
        }
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
