using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    private bool unlocked = false;
    
    //Player stats to save
    public float maxHealth;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (unlocked)
        {
            return;
        }

        if (other.tag == "Player")
        {
            PlayerGeneral player = other.GetComponentInParent<PlayerGeneral>();
            float defaultHealth = player.defaultHealth;
            maxHealth = player.maxHealth;

            player.LastHearth = gameObject;
            //player.history.Clear();
            maxHealth = Mathf.Max(maxHealth, defaultHealth);
            unlocked = true;
        }
    }
}
