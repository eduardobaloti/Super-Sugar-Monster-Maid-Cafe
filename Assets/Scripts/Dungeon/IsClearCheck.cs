using System.Diagnostics.Tracing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsClearCheck : MonoBehaviour

{
    Vector2 position;
    Vector2 mapRange = new Vector2(4, 4);
    public GameObject[] doorStates;
    public GameObject[] monsters;
    public GameObject[] monsterSpawn;
    private int enemyQuantity;

    void Start()
    {
        enemyQuantity = Random.Range(3, 4);

        for (int i = 0; i < enemyQuantity; i++)
        {
            //monsterSpawn[i] = Instantiate(monsters[0]);
        }
    }


    void Update()
    {
        print(monsterSpawn);

        if (monsterSpawn.Length == 0)
        {
            foreach (GameObject door in doorStates)
            {
                door.GetComponent<DoorScript>().doorIsLocked = 1;
            }
        }
    }
}
