using System.Net.Mime;
using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //Player Stats
    Rigidbody2D rb;
    Vector3 move;
    public float speed = 10f;
    public Image[] health;
    public int currentHealth;
    float attackSpeed = 0.35f;


    //Attack methods
    public Transform attackPoint;
    public float attackRange;
    bool isAttacking = false;
    //Layers and songs
    public LayerMask enemyLayers;
    public Animator animator;
    public AudioSource source;
    public AudioClip slash, hitted, item;

    void Start()
    {
        currentHealth = 3;
        rb = GetComponent<Rigidbody2D>();
        Physics.IgnoreLayerCollision(5, 6); //Ui and Enemies?
    }
    void FixedUpdate()
    {
        move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        rb.MovePosition(transform.position + (move * Time.deltaTime * speed));
    }

    void Update()
    {
        if (Input.GetMouseButton(0) || Input.GetButton("Fire1"))
        {
            if (!isAttacking) StartCoroutine(Attack());
        }

        //Aniamtor Settings -----------------------------------

        animator.SetFloat("horizontal", move.x);
        animator.SetFloat("vertical", move.y);
        animator.SetFloat("speed", move.magnitude);

        if (move.x > 0) animator.SetInteger("IsFacing", 1); //Right
        if (move.x < 0) animator.SetInteger("IsFacing", 0); //left

    }

    IEnumerator Attack()
    {
        isAttacking = true;
        source.PlayOneShot(slash);
        animator.SetTrigger("Attack");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("Hitted enemies = " + hitEnemies.Length);
            enemy.GetComponent<GenericMonster>().TakeDamage(1);
        }

        yield return new WaitForSecondsRealtime(attackSpeed);
        isAttacking = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Monster")
        {
            source.PlayOneShot(hitted);
            StartCoroutine(Hitted());
            //Coroutine for knockout
            currentHealth -= 1;
        }
    }

    private IEnumerator Hitted()
    {
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.3f, 0.3f);
        yield return new WaitForSeconds(0.25f);
        this.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }


    void OnDrawGizmosSelected()
    {
        if (attackPoint.position == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void IsLive()
    {
        switch (currentHealth)
        {
            case 3: health[3].color = new Color(1f, 1f, 1f, 0.25f); break;
            case 2: health[2].color = new Color(1f, 1f, 1f, 0.25f); break;
            case 1: health[1].color = new Color(1f, 1f, 1f, 0.25f); break;
            case 0: health[0].color = new Color(1f, 1f, 1f, 0.25f); break;

        }
    }
}
