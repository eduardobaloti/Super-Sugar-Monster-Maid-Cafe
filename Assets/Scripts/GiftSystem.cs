using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftSystem : MonoBehaviour
{
    public GameObject[] itens;

    public void TakeDamage()
    {
        print("gift");
        int randomNumber = Random.Range(0, itens.Length);
        Instantiate(itens[randomNumber], gameObject.transform.position, Quaternion.identity);
        //source.PlayOneShot(beated);
        Destroy(this);
    }

}
