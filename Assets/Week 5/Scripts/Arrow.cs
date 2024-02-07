using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float damage = 1;
    public float inital_vel = 100;
    Rigidbody2D rb;

    void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
        Vector2 force = new Vector2(0, inital_vel);
        rb.AddRelativeForce(force);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
    }

    private void OnBecameInvisible() 
    {
        Destroy(gameObject);
    }
}
