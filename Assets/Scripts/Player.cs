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
    [Header("Player Stats")]
    Rigidbody2D rb;
    Vector3 move;
    public float speed;
    public Image[] health;
    public int maxhealth = 3;
    public int currentHealth;
    public float attackSpeed = 0.30f;


    [Header("Attack stats")]
    public Transform attackPoint;
    public float attackRange;
    bool isAttacking = false;
    bool isHitted = false;
    public enum PowerName
    {
        Default,
        IceCream,
    };
    public PowerName power;
    public GameObject states;


    [Header("Layer and songs")]
    public LayerMask enemyLayers;
    public Animator animator;
    public AudioSource source;
    public AudioClip slash, hitted;

    void Start()
    {
        currentHealth = maxhealth;
        rb = GetComponent<Rigidbody2D>();
        Physics.IgnoreLayerCollision(5, 6); //Ui and EnemyCheck
    }

    void FixedUpdate()
    {
        move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        //move.Normalize();
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
            if (gameObject.tag == "item")
            {
                //print("item attached");
                //enemy.GetComponent<GiftSystem>().TakeDamage();
            }
            else
            {
                Debug.Log("Hitted enemies = " + hitEnemies.Length);
                enemy.GetComponent<GenericMonster>().TakeDamage(1, gameObject);

                //if (power == "ice") states.GetComponent<States>().Frozen();
            }
        }
        yield return new WaitForSecondsRealtime(attackSpeed);
        isAttacking = false;
    }


    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Monster" && isHitted == false)
        {
            source.PlayOneShot(hitted);
            StartCoroutine(Hitted());
            StartCoroutine(Hurted());
            knockback(collision);
            currentHealth -= 1;
            IsLive();
        }
    }

    void knockback(Collision2D enemy)
    {
        Vector2 dist = (gameObject.transform.position - enemy.transform.position);
        dist.Normalize();
        rb.AddForce(dist * 7.5f);
    }

    IEnumerator Hitted()
    {
        
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.3f, 0.3f);
        yield return new WaitForSeconds(0.25f);
        this.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }

    IEnumerator Hurted()
    {
        var oldSpeed = speed;
        speed = speed * 0.5f; 
        isHitted = true;
        yield return new WaitForSeconds(0.35f);
        isHitted = false;
        speed = oldSpeed;
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
