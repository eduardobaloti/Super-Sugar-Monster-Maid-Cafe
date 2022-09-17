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

    void Start()
    {
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
        if (Input.GetMouseButtonDown(0) || Input.GetButtonDown("Fire1"))
        {
            Attack();
        }

        if (Input.GetKeyDown(KeyCode.D) && move.x < 0)
        {
            direction = "right";
            animator.SetTrigger("moveright");
            print("right");
        }
        if (Input.GetKeyDown(KeyCode.A) && move.x > 0)
        {
            direction = "left";
            
            animator.SetTrigger("moveleft");
            print("left");
        }
        if (Input.GetKeyDown(KeyCode.S) && move.y < 0)
        {
            direction = "bottom";
            animator.SetTrigger("movebottom");
            animator.ResetTrigger("movetop");
            print("bottom");
        }
        if (Input.GetKeyDown(KeyCode.W) && move.y > 0)
        {
            direction = "top";
            animator.SetTrigger("movetop");
            animator.ResetTrigger("movebottom");
            print("top");
        }
    }

    void Attack()
    {
        if (direction == "bottom")
        {
            animator.SetTrigger("attackbottom"); ;
        }
        if (direction == "top")
        {
            animator.SetTrigger("attacktop"); ;
        }
        if (direction == "right")
        {
            animator.SetTrigger("attackright"); ;
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

    void OnDrawGizmosSelected()
    {
        if (attackPoint.position == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
