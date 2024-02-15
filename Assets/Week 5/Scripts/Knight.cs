
using UnityEngine;
using UnityEngine.EventSystems;

public class Knight : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    Vector2 destination; 
    Vector2 movement;
    public Vector2 InitalDestination;
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
        destination = InitalDestination; //destination used to initalize to 0, 0 which would cause the knight to walk to the centre upon spawning (which isnt noticable when spawned at 0, 0)
                                        //having the effect of walking as soon as the scene starts (ie from outside the screen) was nice so i added a way to set where the player initally walks
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
        setHealth(health - damage);
    }

    public void setHealth(float h)
    {
         health = Mathf.Clamp(h, 0, maxhealth);
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
