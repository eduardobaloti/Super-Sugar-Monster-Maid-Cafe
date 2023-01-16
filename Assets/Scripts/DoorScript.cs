using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public string direction;
    private GameObject cameratp;
    public Sprite[] doorState;
    public int doorIsLocked = 0;

    void Start()
    {
        cameratp = GameObject.FindGameObjectWithTag("MainCamera");

        Sprite door = this.gameObject.GetComponent<Sprite>();
        if (doorIsLocked == 1) door = doorState[1];
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && doorIsLocked == 1)
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
                other.transform.position += new Vector3(0, 2.4f);
                cameratp.transform.Translate(new Vector3(0, 3.75f));
            }

            if (direction == "bottom")
            {
                other.transform.position -= new Vector3(0, 2.4f);
                cameratp.transform.Translate(new Vector3(0, -3.75f));
            }

        }
    }
}
