using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAbilities : MonoBehaviour {
	private EnemyMovement em;

    public Transform playerTransform;
    public bool enraged = false;

    public Rigidbody2D spike;
    private float force = 15;
	private int wallCount = 0;

    private Transform[] fire = new Transform[5];
    public Transform firePos0;
    public Transform firePos1;
    public Transform firePos2;
    public Transform firePos3;
    public Transform firePos4;

    float maxCd = 5f;
    float spikeCd;
	float chargeCd;
    int enrageHp = 30;

    // Use this for initialization
    void Start () {
        fire[0] = firePos0;
        fire[1] = firePos1;
        fire[2] = firePos2;
        fire[3] = firePos3;
        fire[4] = firePos4;

		em = GetComponentInParent<EnemyMovement>();
		chargeCd = 10f;
        spikeCd = maxCd;
	}
	
	// Update is called once per frame
	void Update () {
        //Enrage check
        int health = em.health;
        if (health <= enrageHp)
        {
            enraged = true;
            GetComponent<SpriteRenderer>().color = new Color(health / 100f, health / 100f, 1, 1);
        }

		if (spikeCd <= 0 && playerTransform.position.y > transform.position.y) {
			launchSpikes ();
		} else if (chargeCd <= 0 && em.charge == false) {
			charge ();
		} else
        {
			chargeCd -= Time.deltaTime;
            spikeCd -= Time.deltaTime;
        }
	}


    //launches spikes in all directions.
    //Spikes collide with terrain or the player.
    void launchSpikes()
    {
        if (enraged)
        {
            enragedSpikes();
        }
        else
        {
            StartCoroutine(launch());
            spikeCd = maxCd;
        }
    }

    IEnumerator launch()
    {
        foreach (Transform firepos in fire)
        {
            Rigidbody2D spikeInstance = Instantiate(spike, firepos.position, firepos.rotation) as Rigidbody2D;
            spikeInstance.velocity = force * firepos.right;
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator launchDouble()
    {
        StartCoroutine(launch());
        yield return new WaitForSeconds(1f);
        StartCoroutine(launch());
    }

    IEnumerator launchCombo()
    {
        StartCoroutine(launchDouble());
        yield return new WaitForSeconds(3f);
        branchFall();
        yield return new WaitForSeconds(3f);
        StartCoroutine(launchDouble());
    }

    //launches spikes 2 times
    //waits 3 seconds
    //branchfall
    //launches spikes 2 times
    void enragedSpikes()
    {
        StartCoroutine(launchDouble());   
    }

    //Charges to the wall on whichever direction it is facing.
    //Gets stunned for 3 seconds.
    void charge()
    {
        if (enraged)
        {
            enragedCharge();
        }
        else
        {
            em.rigidbody.velocity = new Vector2(em.dir * 6f, 0);
        }
		em.charge = true;
    }

    //Charges to the wall, then charges to the other. Repeat 3 times.
    void enragedCharge()
    {
        em.rigidbody.velocity = new Vector2(em.dir * 10f, 0);
    }

    //Charges* at the tree. Makes a layer of branches (spikes) fall.
    void branchFall()
    {

    }

	private void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Environment" && em.charge == true)
		{
			wallCount += 1;
			if (!enraged || wallCount == 3) {
				wallCount = 0;
				chargeCd = 10f;
				em.chargeTimer = 3f;
				em.rigidbody.velocity = new Vector2 (0, 0);
				em.charge = false;
				enraged = false;
			} else {
				Vector3 temp = em.gameObject.transform.localScale;
				temp.x = -temp.x;
				em.gameObject.transform.localScale = temp;
				em.rigidbody.velocity = new Vector2 (em.rigidbody.velocity.x * -1, 0);
			}
		}
	}
}
