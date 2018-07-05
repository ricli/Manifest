using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttack : MonoBehaviour {

    private Rigidbody2D rigidbody;

	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody2D>();
        if (Vector2.Dot(new Vector2(1, 0), rigidbody.velocity) < 1)
        {
            Vector3 temp = gameObject.transform.localScale;
            temp.x = -temp.x;
            gameObject.transform.localScale = temp;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Environment" || other.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}