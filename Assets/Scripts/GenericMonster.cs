using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericMonster : MonoBehaviour
{
    Rigidbody2D rb;

    public Animator animator;

    public int maxHealth = 1000;
    public int currentHealth;
    Vector2 move;
    Vector2 maidPosition;

    public Transform maid;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;  
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        move = new Vector2(0.005f, 0.005f) * Time.deltaTime; 
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "wall")
        {
            move = new Vector2(-0.005f, 0f);
            print("colid");
        }

         if (collision.gameObject.tag == "player")
        {
            /*
            move = new Vector2(-0.005f, 0f);
            print("colid");
            */
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("damage");

        if(currentHealth <= 0)
        {
            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            this.enabled = false;
        }
    }
}
