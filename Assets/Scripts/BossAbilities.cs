using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossAbilities : MonoBehaviour {
	private EnemyMovement em;

    public GameObject trunk;

    public Slider slider;

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
    float branchFallCd;

    int health;
    int enrageHp = 30;

    // Use this for initialization
    void Start () {
        fire[0] = firePos0;
        fire[1] = firePos1;
        fire[2] = firePos2;
        fire[3] = firePos3;
        fire[4] = firePos4;

		em = GetComponentInParent<EnemyMovement>();
        health = em.health;
        slider.maxValue = health;

        //Cooldowns
		chargeCd = 10f;
        spikeCd = maxCd;
        branchFallCd = 10f;
	}
	
	// Update is called once per frame
	void Update () {
        int tempDir = em.dir;
        Vector3 tempScale = transform.localScale;
        if (tempDir > 0 && tempScale.x < 0)
        {
            tempScale.x = -tempScale.x;
        } else if (tempDir < 0 && tempScale.x > 0)
        {
            tempScale.x = -tempScale.x;
        }
        transform.localScale = tempScale;

        health = em.health;
        slider.value = health;

        //Enrage check
        if (health <= enrageHp)
        {
            enraged = true;
            GetComponent<SpriteRenderer>().color = new Color(health / 100f, health / 100f, 1, 1);
        }

		if (spikeCd <= 0 && playerTransform.position.y > transform.position.y) {
			launchSpikes ();
		} else if (chargeCd <= 0 && em.charge == false && playerTransform.position.y <= transform.position.y + 2) {
			charge ();
		} else if (branchFallCd <= 0 && playerTransform.position.y > transform.position.y + 8)
        {
            branchFall();
        } else
        {
			chargeCd -= Time.deltaTime;
            spikeCd -= Time.deltaTime;
            branchFallCd -= Time.deltaTime;
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

    public void stopMomentum()
    {
        branchFallCd = 10f;
        em.branchFall = false;
        em.rigidbody.velocity = new Vector2(0, 0);
        Rigidbody2D debris3 = (Rigidbody2D)Instantiate(spike, new Vector3(234.5f, -14, 0), Quaternion.identity);
        Rigidbody2D debris4 = (Rigidbody2D)Instantiate(spike, new Vector3(236, -14, 0), Quaternion.identity);
        Rigidbody2D debris5 = (Rigidbody2D)Instantiate(spike, new Vector3(237.5f, -14, 0), Quaternion.identity);
        Rigidbody2D debris6 = (Rigidbody2D)Instantiate(spike, new Vector3(239, -14, 0), Quaternion.identity);
        Rigidbody2D debris7 = (Rigidbody2D)Instantiate(spike, new Vector3(240.5f, -14, 0), Quaternion.identity);
        Rigidbody2D debris8 = (Rigidbody2D)Instantiate(spike, new Vector3(242, -14, 0), Quaternion.identity);
        Rigidbody2D debris1 = (Rigidbody2D)Instantiate(spike, new Vector3(217, -14, 0), Quaternion.identity);
        Rigidbody2D debris2 = (Rigidbody2D)Instantiate(spike, new Vector3(218.5f, -14, 0), Quaternion.identity);
        Rigidbody2D debris9 = (Rigidbody2D)Instantiate(spike, new Vector3(220, -14, 0), Quaternion.identity);
        Rigidbody2D debris10 = (Rigidbody2D)Instantiate(spike, new Vector3(221.5f, -14, 0), Quaternion.identity);
        Rigidbody2D debris11 = (Rigidbody2D)Instantiate(spike, new Vector3(223, -14, 0), Quaternion.identity);
        Rigidbody2D debris12 = (Rigidbody2D)Instantiate(spike, new Vector3(225, -14, 0), Quaternion.identity);
        Rigidbody2D debris13 = (Rigidbody2D)Instantiate(spike, new Vector3(244, -14, 0), Quaternion.identity);
    }

    //Charges* at the tree. Makes a layer of branches (spikes) fall.
    void branchFall()
    {
        if (gameObject.transform.position.x < 222f || gameObject.transform.position.x > 237f)
        {
            if (trunk.transform.position.x - gameObject.transform.position.x < 0)
            {
                if (em.dir > 0)
                {
                    em.dir = -em.dir;
                    Vector3 temp = transform.localScale;
                    temp.x = -temp.x;
                    transform.localScale = temp;
                }
            }
            else if (trunk.transform.position.x - gameObject.transform.position.x > 0)
            {
                if (em.dir < 0)
                {
                    em.dir = -em.dir;
                    Vector3 temp = transform.localScale;
                    temp.x = -temp.x;
                    transform.localScale = temp;
                }
            }
            em.branchFall = true;
            em.rigidbody.velocity = new Vector2(em.dir * 6f, 0);
        }
        else
        {
            branchFallCd = 10f;
        }
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
