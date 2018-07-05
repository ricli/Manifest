using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour {

    //private Dictionary<GameObject, Transform> history;
   // public GameObject player;

    /* Use this for initialization
    void Start()
    {
        history = player.GetComponent<PlayerGeneral>().history;
    }
    */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == ("Player"))
        {
            collision.GetComponentInParent<PlayerGeneral>().pickUpFlame();
            //history.Add(gameObject, transform);
            Destroy(this.gameObject);
        }
    }
}
