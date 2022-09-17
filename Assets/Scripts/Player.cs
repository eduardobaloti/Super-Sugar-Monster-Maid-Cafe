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


    enum pos{
        top,
        bottom,
        left,
        right,
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Physics.IgnoreLayerCollision(5,6);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        rb.MovePosition(transform.position + (move * Time.deltaTime * speed));   
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetButtonDown("Fire1"))
        {
            Attack();
        }

        //if (Input.GetKeyDown(KeyCode.W)) {pos}
        


    }
    
    void Attack()
    {
        //slash.PlayOneShot(slash.clip, 1);        
        animator.SetTrigger("attack");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach(Collider2D enemy in hitEnemies)
        {
            Debug.Log("Hited");

            enemy.GetComponent<GenericMonster>().TakeDamage(1);
            

            
        }


    }

    void OnDrawGizmosSelected() {

        if (attackPoint.position == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
/*
    void OnCollisionEnter(Collision other) 
    {
        //MonsterColision    
    }

*/
}
                       