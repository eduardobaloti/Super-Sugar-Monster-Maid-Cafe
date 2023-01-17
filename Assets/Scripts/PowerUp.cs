using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum PowerName
    {
        Coffe,
        CakePiece,
        Cake,
        IceCream
    }

    public PowerName power;
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

            if (power == PowerName.Coffe)
            {
                frame.sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
                maid.GetComponent<Player>().speed += 0.30f;
                maid.GetComponent<Player>().attackSpeed = 0.25f;
            }

            if (power == PowerName.CakePiece)
            {
                if (maid.GetComponent<Player>().currentHealth == maid.GetComponent<Player>().maxhealth) { }
                else
                {
                    maid.GetComponent<Player>().currentHealth += 1;
                    maid.GetComponent<Player>().health[maid.GetComponent<Player>().currentHealth - 1].color = Color.white;
                }
            }

            if (power == PowerName.IceCream)
            {
                frame.sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
                maid.GetComponent<Player>().power = Player.PowerName.IceCream;
            }

            Destroy(this.gameObject);
        }

    }
}
