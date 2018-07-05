﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponentInParent<PlayerGeneral>().TakeDamage(1);
        }

        if (other.tag != "GameController" && other.tag != "Trunk")
        {
            Destroy(gameObject);
        }
    }
}
