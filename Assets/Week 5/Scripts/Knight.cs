
using UnityEngine;
using UnityEngine.EventSystems;

public class Knight : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    Vector2 destination; 
    Vector2 movement;
    bool clickOnSelf = false;
    public float speed = 3;
    public float health;
    public float maxhealth = 5;
    bool isDead = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        health = maxhealth;
    }

    private void FixedUpdate() 
    {
        if(isDead) return;
        movement = destination - (Vector2)transform.position;
        if(movement.magnitude < 0.1) 
        {
            movement = Vector2.zero;
        }
        rb.MovePosition(rb.position + movement.normalized * speed * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        if(isDead) return;
        if (Input.GetMouseButtonDown(0) && !clickOnSelf && !EventSystem.current.IsPointerOverGameObject()) 
        {
            destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetMouseButtonDown(1)) 
        {
            animator.SetTrigger("Attack");
        }
        animator.SetFloat("movement", movement.magnitude);
    }

    private void OnMouseDown() 
    {
        if(isDead) return;
        clickOnSelf = true;
        SendMessage("TakeDamage", 1);
    }

    private void OnMouseUp() 
    {
        clickOnSelf = false;
    }

    public void TakeDamage(float damage)
    {   
        health -= damage;
        health = Mathf.Clamp(health, 0, maxhealth);
        if(health == 0) 
        {
            animator.SetTrigger("Death");
            isDead = true;

        }
        else
        {
            isDead = false;
            animator.SetTrigger("TakeDamage");
        }
    }

}
