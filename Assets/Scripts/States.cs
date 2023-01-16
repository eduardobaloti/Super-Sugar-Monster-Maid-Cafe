using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class States : MonoBehaviour
{
    public GameObject iceCube;
    public GameObject enemyteste;


    public IEnumerator Frozen()
    {
        print("frozen");
        var ice = Instantiate(iceCube);
        ice.transform.parent = enemyteste.transform;
        enemyteste.gameObject.GetComponent<GenericMonster>().speed = 0f;
        yield return new WaitForSecondsRealtime(3);
        Destroy(ice);
        enemyteste.gameObject.GetComponent<GenericMonster>().speed = 1f;
    }

}
