using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public string direction;
    private GameObject cameratp;
    public Sprite[] doorState;


    void Start()
    {
        cameratp = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (direction == "left")
            {
                other.transform.position -= new Vector3(2.3f, 0);
                cameratp.transform.Translate(new Vector3(-5, 0));
            }

            if (direction == "right")
            {
                other.transform.position += new Vector3(2.3f, 0);
                cameratp.transform.Translate(new Vector3(5f, 0));
            };

            if (direction == "top")
            {
                other.transform.position += new Vector3(0, 2.3f);
                cameratp.transform.Translate(new Vector3(0, 3.75f));
            }

            if (direction == "bottom")
            {
                other.transform.position -= new Vector3(0, 2.3f);
                cameratp.transform.Translate(new Vector3(0, -3.75f));
            }

        }
    }
}
