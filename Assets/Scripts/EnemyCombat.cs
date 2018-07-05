using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour {

    private EnemyMovement self;
	private Transform selfTransform;
    private int damage = 1;
    private Vector2 pushForward = new Vector2 (8, 4);
	private Vector2 pushBack = new Vector2 (-8, 4);

	private int constKnockback;

    private bool stillInContact = false;

	// Use this for initialization
	void Start () {
        self = GetComponentInParent<EnemyMovement>();
		selfTransform = self.GetComponentInParent<Transform> ();
		constKnockback = 15;
	}
	
	// Update is called once per frame
	void Update () {

	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            stillInContact = true;
            Rigidbody2D target = other.GetComponent<Rigidbody2D>();	
			PlayerNormal pn = other.GetComponentInChildren<PlayerNormal> ();

            PlayerGeneral player = target.GetComponentInParent<PlayerGeneral>();
            player.TakeDamage(damage);
            StartCoroutine(delayedDamage(player));

            if (player.currentHealth > 0)
            {
                if (target.position.x < selfTransform.position.x)
                {
                    target.velocity = pushBack;
                }
                else
                {
                    target.velocity = pushForward;
                }

                if (pn != null)
                {
                    pn.knockbackTime = constKnockback;
                }
            }
        }

        if (other.tag == "Purification")
        {
            self.TakeDamage(1);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            stillInContact = false;
        }
    }

    IEnumerator delayedDamage(PlayerGeneral player)
    {
        yield return new WaitForSeconds(2f);
        while (stillInContact)
        {
            player.TakeDamage(damage);
            yield return new WaitForSeconds(2f);
        }

    }
}
