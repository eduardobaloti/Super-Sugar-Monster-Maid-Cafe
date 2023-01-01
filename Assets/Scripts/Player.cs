using System.Net.Mime;
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

    //Attack methods
    public Transform attackPoint;
    public float attackRange = 0.6f;
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
        animator.SetFloat("horizontal", move.x);
        animator.SetFloat("vertical", move.y);
        animator.SetFloat("speed", move.magnitude);

        IsLive();
    }

    void Attack()
    {
        rb.AddForce(new Vector2(0f, 0f));

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
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint.position == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    void IsLive()
    {
        switch (currentHealth)
        {
            case 3:
                //GameObject.Find("lf0").SetActive(false);
                break;
            case 2:
                //GameObject.Find("lf1").GetComponent<SpriteRenderer>.enabled = false;
                break;
            case 1:
                GameObject.Find("lf2").SetActive(false);
                break;
            case 0:
                GameObject.Find("lf3").SetActive(false);
                GetComponent<Collider2D>().enabled = false;
                GetComponent<SpriteRenderer>().enabled = false;
                GameObject restart = GameObject.FindGameObjectWithTag("Restart");
                restart.transform.GetChild(0).gameObject.SetActive(true);
                break;
        }
    }
}
