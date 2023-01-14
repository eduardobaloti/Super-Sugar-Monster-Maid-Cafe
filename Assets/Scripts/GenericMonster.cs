using System.Transactions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericMonster : MonoBehaviour
{
    Rigidbody2D rb;
    public Animator animator;

    //Monster stats
    int maxHealth = 3;
    int currentHealth;


    public Transform maid;


    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        //Check speed and flip monster

        //PatrolWait();
        rb.MovePosition(transform.position + (maid.position * 0.25f * Time.deltaTime));
        //PatrolWait();                  
        //rb.MovePosition(transform.position + (new Vector3(-1, 0) * Time.deltaTime * 2));
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
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
        StartCoroutine(Hitted());

        if (currentHealth <= 0)
        {
            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            Destroy(this);
        }
    }

    private IEnumerator Hitted()
    {
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.3f, 0.3f);
        yield return new WaitForSeconds(0.25f);
        this.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }
}
