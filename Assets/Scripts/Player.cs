using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //public AudioSource slash; 

    Rigidbody2D rb;
    public float speed = 10f;
    bool walking = false;


    //Attack methods
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public Animator animator;
    Vector3 move;
    String direction = "bottom";

    public int maxHealth = 3;
    public int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;

        rb = GetComponent<Rigidbody2D>();
        Physics.IgnoreLayerCollision(5, 6);
    }
    void FixedUpdate()
    {
        move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        rb.MovePosition(transform.position + (move * Time.deltaTime * speed));
    }

    void Update()
    {
        print(move);
        if (Input.GetMouseButtonDown(0) || Input.GetButtonDown("Fire1"))
        {
            Attack();
        }
        if (Input.GetKeyDown(KeyCode.D) && move.x > 0.0f)
        {
            direction = "right";
            animator.SetTrigger("moveright");

        }
        if (Input.GetKeyDown(KeyCode.A) && move.x < 0.0f)
        {
            direction = "left";
            animator.SetTrigger("moveleft");


        }
        if (Input.GetKeyDown(KeyCode.S) && move.y < 0.0f)
        {
            direction = "bottom";
            animator.SetTrigger("movebottom");
        }
        if (Input.GetKeyDown(KeyCode.W) && move.y > 0.0f)
        {
            direction = "top";
            animator.SetTrigger("movetop");
        }
    }

    void Attack()
    {
        rb.AddForce(new Vector2(0f,0f));

        if (direction == "bottom")
        {
            animator.SetTrigger("attackbottom"); ;
        }
        if (direction == "top")
        {
            animator.SetTrigger("attacktop");
        }
        if (direction == "right")
        {
            animator.SetTrigger("attackright");
        }
        if (direction == "left")
        {
            animator.SetTrigger("attackleft"); ;
        }

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {

            Debug.Log("Hited");
            enemy.GetComponent<GenericMonster>().TakeDamage(1);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("player collision");
        Vector3 damage = new Vector3(0, -5);
        if (collision.gameObject.tag == "Monster" && (direction == "top"))
        {
            rb.MovePosition(transform.position + damage);
            currentHealth -= 1;
        }
        if (collision.gameObject.tag == "Monster" && (direction == "bottom"))
        {
            //rb.MovePosition(transform.position + damage);
            currentHealth -= 1;
        }
        if (collision.gameObject.tag == "Monster" && (direction == "right"))
        {
            //rb.MovePosition(transform.position + damage);
            currentHealth -= 1;
        }
        if (collision.gameObject.tag == "Monster" && (direction == "left"))
        {
            //rb.MovePosition(transform.position + damage);
            currentHealth -= 1;
        }
        

        if (currentHealth == 3)
        {
            //GameObject.Find("lf3").SetActive(false);
        }
        if (currentHealth == 2)
        {
            GameObject.Find("lf1").SetActive(false);
        }
        if (currentHealth == 1)
        {
            GameObject.Find("lf2").SetActive(false);
        }

        if (currentHealth <= 0)
        {
            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            Destroy(this);

            GameObject.Find("lf3").SetActive(false);
            GameObject restart = GameObject.FindGameObjectWithTag("Restart");
            restart.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint.position == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);

    }
}
