using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public string powerName;
    public SpriteRenderer frame;
    public GameObject maid;
    public GameObject eventSystem;

    void Start()
    {
        eventSystem = GameObject.Find("EventSystem");
    }

    void OnCollisionEnter2D(Collision2D player)
    {
        if (player.gameObject.tag == "Player")
        {
            eventSystem.GetComponent<GameFlow>().PowerUp();

            if (powerName == "coffe")
            {
                frame.sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
                maid.GetComponent<Player>().speed += 0.30f;
                maid.GetComponent<Player>().attackSpeed = 0.25f;
            }

            if (powerName == "cake")
            {
                if (maid.GetComponent<Player>().currentHealth == maid.GetComponent<Player>().maxhealth) { }
                else
                {
                    maid.GetComponent<Player>().currentHealth += 1;
                    maid.GetComponent<Player>().health[maid.GetComponent<Player>().currentHealth - 1].color = Color.white;
                }
            }

            if (powerName == "icecream")
            {
                frame.sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
                maid.GetComponent<Player>().power = "ice";
            }

            Destroy(this.gameObject);
        }

    }
}
