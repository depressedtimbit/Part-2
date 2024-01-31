using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Plane : MonoBehaviour
{
    public List<Vector2> points;
    public List<Sprite> sprites;
    public float newPointThreshold = 0.2f;
    public float speed = 1;
    public Vector2 spawnCentre;
    public float spawnArea;
    Vector2 lastPos;
    LineRenderer lineRenderer;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    Vector2 currentPos;
    GameObject WarnTri;
    public AnimationCurve landingCurve;
    float landingTimer;
    bool isLanding;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lineRenderer = GetComponent<LineRenderer>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        WarnTri = transform.GetChild(0).gameObject;

        Sprite randomSprite =  sprites[Random.Range(0, sprites.Count)];

        spriteRenderer.sprite = randomSprite;

        Vector2 spawnPos = new Vector2(
            Random.Range(spawnCentre.x-spawnArea, spawnCentre.x+spawnArea),
            Random.Range(spawnCentre.y-spawnArea, spawnCentre.y+spawnArea)
            );
        rb.position = spawnPos;
        Vector2 direction = Vector2.zero - spawnPos;
        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        rb.rotation = -angle;
        

        
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
        if(isLanding)
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

    void OnTriggerEnter2D()
    {
        WarnTri.SetActive(true);
    }
    
    void OnTriggerExit2D()
    {
        WarnTri.SetActive(false);
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if(Vector3.Distance(transform.position, collision.gameObject.transform.position)<0.5) 
        {
            isLanding = true;
        }
        

    }
    
}
