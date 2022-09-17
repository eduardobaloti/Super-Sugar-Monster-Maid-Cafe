using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public string direction;
    private GameObject cameratp;

    private void Start()
    {
        cameratp = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (direction == "left")
            {
                other.transform.position += new Vector3(5f, 0);
                cameratp.transform.position += new Vector3(0, -5);
            }
                

            print("doored");
        }
        if (other.CompareTag("Player"))
        {
            if (direction == "right")
            {
                other.transform.position += new Vector3(-5f, 0);
                cameratp.transform.position += new Vector3(0, -5);
            }


            print("doored");
        }
        if (other.CompareTag("Player"))
        {
            if (direction == "top")
            {
                other.transform.position += new Vector3(0, 2.5f);
                cameratp.transform.position += new Vector3(0, 3);
            }

            print("doored top");
        }
        if (other.CompareTag("Player"))
        {
            if (direction == "bottom")
            {
                other.transform.position += new Vector3(0, -2.5f);
                cameratp.transform.position += new Vector3(0, -3);
                print("doored");
            }

        }
    }
}
