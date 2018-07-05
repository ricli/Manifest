using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGeneral : MonoBehaviour {

    public Image blackout;
    public TextController text;
    private string respawnMessage = "I will not let your fire be extinguished just yet. Spark anew, little sprite! But beware, my power is not infinite.";

    //Controls
    public bool controlsEnabled = true;

    //Purification Utility Variables
    public Rigidbody2D boltTemplate;
    public Transform boltStartPos;

    private float force = 7f;
    private float cd = 1f;
    private float maxCd = 1f;

    //offsets for player and morph relative to playerholder
    private Vector3 playerOffset;
    private Vector3 morphOffset;

    //CurrentHealth = morphDuration, and will recharge to max; Max decreases only with damage
    public int tries = 3;

    public float defaultHealth;
    public float maxHealth;
    public float currentHealth;

    //Slider to show duration of Morph for now
    public Slider morphBar;

	// Tail changes
	public static ParticleSystem tail;


    //State check
    private bool isMorphed;
    private Transform playerTransform;

    //Last Checkpoint
    public GameObject LastHearth;
    public Dictionary<GameObject, Transform> history = new Dictionary<GameObject, Transform>();

	public GameObject flameOnHead;
    Transform fireTransform;
    Transform innerFireTransform;
    Vector3 defaultFireScale;

    private void Awake()
    {
        playerOffset = new Vector3(0, 0, 0);
        morphOffset = new Vector3(-0.2039995f, -0.7250001f, 0);
        maxHealth = defaultHealth;
        currentHealth = defaultHealth;
        morphBar.maxValue = defaultHealth;
        isMorphed = false;
        playerTransform = GetComponent<Transform>();
        fireTransform = flameOnHead.transform.Find("Fire").transform;
        innerFireTransform = flameOnHead.transform.Find("Inner Fire").transform;
        defaultFireScale = fireTransform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && controlsEnabled)
        {
            Morph();
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Revert();
        }
        if (isMorphed)
        {
            currentHealth -= 1.5f * Time.deltaTime;
            if (currentHealth <= 1f)
            {
                currentHealth = 1f;
                Revert();
            }
            morphBar.value = currentHealth;


            Vector3 location = playerTransform.position;
            location.x += playerTransform.Find("ParticleSystem-Morph").transform.localPosition.x - morphOffset.x;
            location.y += playerTransform.Find("ParticleSystem-Morph").transform.localPosition.y - morphOffset.y;
            playerTransform.position = location;
            playerTransform.Find("ParticleSystem-Morph").transform.localPosition = morphOffset;
        } else
        {
            if (playerTransform.Find("Player").GetComponent<PlayerNormal>().isGrounded)
            {
                if (currentHealth < maxHealth)
                {
                    currentHealth += 2f * Time.deltaTime;
                    if (currentHealth > maxHealth)
                    {
                        currentHealth = maxHealth;
                    }
                }
                morphBar.value = currentHealth;

            }

            if (Input.GetKeyDown(KeyCode.E) && controlsEnabled)
            {
                if (cd <= 0 && currentHealth > 1.5)
                {
                    Purify();
                    currentHealth -= 1;
                }
            }

            //update playerholder transform relative to player
            Vector3 location = playerTransform.position;
            location.x += playerTransform.Find("Player").transform.localPosition.x - playerOffset.x;
            location.y += playerTransform.Find("Player").transform.localPosition.y - playerOffset.y;
            playerTransform.position = location;
            playerTransform.Find("Player").transform.localPosition = playerOffset;
        }

        //Decrease Purify Cooldown
        if (cd > 0)
        {
            cd -= Time.deltaTime;
        }
    }

    //Deactivates dude, activates morph
    void Morph()
    {
        if (!isMorphed)
        {
            playerTransform.Find("Player").gameObject.SetActive(false);
            playerTransform.Find("ParticleSystem-Morph").gameObject.SetActive(true);
			playerTransform.Find ("FlameOnHead").gameObject.SetActive (false);
            isMorphed = true;
        }
    }

    //Deactivates morph, activates dude
    void Revert()
    {
        if (isMorphed)
        {

			GameObject playerGameObject = playerTransform.Find ("Player").gameObject;
            playerGameObject.SetActive(true);

			// Momentum for morph -> human
			Rigidbody2D playerRigidBody = playerTransform.Find ("Player").gameObject.GetComponent<Rigidbody2D>();
			Rigidbody2D morphRigidBody = playerTransform.Find ("ParticleSystem-Morph").gameObject.GetComponent<Rigidbody2D>();
            Vector3 temp = morphRigidBody.velocity;
            temp.y = 0;
			playerRigidBody.velocity = temp;

            playerTransform.Find("ParticleSystem-Morph").gameObject.SetActive(false);
			playerTransform.Find ("FlameOnHead").gameObject.SetActive (true);
            isMorphed = false;
        }
    }

    void Purify()
    {
        int dirFacing = GetComponentInChildren<PlayerNormal>().dirFacing;
        if (dirFacing == 1)
        {
            Rigidbody2D bolt = Instantiate(boltTemplate, boltStartPos.position, boltStartPos.rotation) as Rigidbody2D;
            bolt.velocity = force * boltStartPos.right * dirFacing;
        } else
        {
            boltStartPos.position += new Vector3(-1, 0, 0);
            Rigidbody2D bolt = Instantiate(boltTemplate, boltStartPos.position, boltStartPos.rotation)  as Rigidbody2D;
            boltStartPos.position += new Vector3(1, 0, 0);
            bolt.velocity = force * boltStartPos.right * dirFacing;
        }
        cd = maxCd;
    }

    //Reduce flame by the amount of damage received. If dead, respawn.
    //This method should be called through the script of the source of the damage.
    public void TakeDamage(int damage)
    {
        Vector3 curScale = fireTransform.localScale;

        fireTransform.localScale = new Vector3(curScale.x - 0.1f, curScale.y, curScale.z - 0.1f);
        innerFireTransform.localScale = new Vector3(curScale.x - 0.1f, curScale.y, curScale.z - 0.1f);

        currentHealth -= (float)damage;
        maxHealth -= (float)damage;
        if (currentHealth <= 0 || maxHealth <= 0)
        {
            Respawn();
        }
    }

    //Add to flame total by picking up flames
    public void pickUpFlame()
    {
    	
		Vector3 curScale = fireTransform.localScale;

		fireTransform.localScale = new Vector3 (curScale.x + 0.1f, curScale.y, curScale.z + 0.1f);
		innerFireTransform.localScale = new Vector3 (curScale.x + 0.1f, curScale.y, curScale.z + 0.1f);

        currentHealth += 1f;
        maxHealth += 1f;
    }

    //Go back to the last Hearth Tree
    void Respawn()
    {
        //Implement Respawn timer
        if (tries > 0)
        {
            StartCoroutine(RespawnSequence());
            /*Respawn everything collected since last checkpoint
            foreach (KeyValuePair<GameObject, Transform> pair  in history)
            {
                Instantiate(pair.Key, pair.Value.position, pair.Value.rotation);
            }
            */
        } else {
            LevelManager levelManager = GameObject.FindObjectOfType<LevelManager>();
            levelManager.LoadLevel("Lose");
        }
    }

    IEnumerator RespawnSequence()
    {
        controlsEnabled = false;
        float alpha = blackout.color.a;
        text.displayMessage(respawnMessage);
        Debug.Log(alpha);
        for (float t = 0f; t < 1f; t += Time.deltaTime / 3f)
        {
            Color newColor = blackout.color;
            newColor.a = Mathf.Lerp(alpha, 1f, t);
            blackout.color = newColor;
            Debug.Log(alpha);
            yield return null;
        }
        transform.position = LastHearth.transform.position;
        float alpha2 = blackout.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 3f)
        {
            Color newColor = blackout.color;
            newColor.a = Mathf.Lerp(alpha, 0f, t);
            blackout.color = newColor;
            Debug.Log(alpha2);
            yield return null;
        }
        Checkpoint checkpoint = LastHearth.GetComponent<Checkpoint>();
        maxHealth = checkpoint.maxHealth;
        currentHealth = maxHealth;
        fireTransform.localScale = defaultFireScale;
        innerFireTransform.localScale = defaultFireScale;
        tries--;
        controlsEnabled = true;
    }

    IEnumerator Timer(int time)
    {
        print(Time.time);
        yield return new WaitForSeconds(time);
        print(Time.time);
    }


}
