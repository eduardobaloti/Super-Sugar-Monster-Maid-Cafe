using System.Transactions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class GenericMonster : MonoBehaviour
{
    Rigidbody2D rb;
    public Animator animator;
    public AudioSource source;
    public AudioClip beated;
    public AIPath ai;
    public Transform maid;


    //Monster stats
    public int maxHealth = 3;
    int currentHealth;
    bool isHitted = false;
    

    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (ai.desiredVelocity.x >= 0.1f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (ai.desiredVelocity.x <= -0.1f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }



    public void TakeDamage(int damage, GameObject player)
    {
        currentHealth -= damage;
        StartCoroutine(Hitted());
        //StartCoroutine(Hurted());
        knockback(player);

        if (currentHealth <= 0)
        {
            source.PlayOneShot(beated);
            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            Destroy(this);
        }
    }
    

    /*
    IEnumerator Hurted()
    {
        var oldSpeed = speed;
        speed = speed * 0.1f;
        isHitted = true;
        yield return new WaitForSeconds(0.25f);
        isHitted = false;
        speed = oldSpeed;
    }
    */

    void knockback(GameObject enemy)
    {
        Vector2 dist = (gameObject.transform.position - enemy.transform.position);
        dist.Normalize();
        rb.AddForce(dist * 5f);
    }

    private IEnumerator Hitted()
    {
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.3f, 0.3f);
        yield return new WaitForSeconds(0.25f);
        this.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }
}
