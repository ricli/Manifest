using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDirection : MonoBehaviour {

    private int dir;
	// Use this for initialization
	void Start () {

        dir = GetComponentInParent<EnemyMovement>().dir;

    }
	
	// Update is called once per frame
	void Update () {
        GetComponentInParent<EnemyMovement>().dir = dir;
	}

    private void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "Environment")
        {
            dir = -dir;
        }
    }
}
