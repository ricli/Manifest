using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
    public GameObject disappearingWall;
    public bool dead;

    //public GameObject flame;
    public PlayerNormal player;
    public int dir = 1;
    public int health;
	public bool charge;
    public bool branchFall;

    private int defaultHealth = 100;
    private int defaultDir = 1;
    private float Speed = 2f;
	public float chargeTimer;
    public Rigidbody2D rigidbody;
    private SpriteRenderer sprite;

    private Transform[] fire = new Transform[5];
    public Transform firePos0;
    public Transform firePos1;
    public Transform firePos2;
    public Transform firePos3;
    public Transform firePos4;

    //private Dictionary<GameObject, Transform> history;

    // Use this for initialization
    void Start () {
        rigidbody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        health = defaultHealth;
		chargeTimer = 0;

        fire[0] = firePos0;
        fire[1] = firePos1;
        fire[2] = firePos2;
        fire[3] = firePos3;
        fire[4] = firePos4;

        health -= player.purifiedAmount * 3;

		charge = false;
        branchFall = false;
        //history = player.GetComponent<PlayerGeneral>().history;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
	{
		if (!charge && !branchFall && chargeTimer <= 0) {
			rigidbody.velocity = new Vector2 (dir * Speed, 0);
			if (dir != defaultDir) {
				Vector3 temp = gameObject.transform.localScale;
				temp.x = -temp.x;
				gameObject.transform.localScale = temp;
				if (fire.Length > 0) {
					foreach (Transform firepos in fire) {
						Vector3 temp1 = firepos.localScale;
						temp1 = -temp1;
						firepos.localScale = temp;
					}
				}
				defaultDir = dir;
			}
		} else {
			if (chargeTimer >= 0) {
				chargeTimer -= Time.deltaTime;
			}
		}
    }

    public void TakeDamage(int damage)
    {
        //rigidbody.AddForce(new Vector2(2, 0));
        health -= damage;
        if (health <= 0)
        {
            //history.Add(gameObject, transform);
            //Instantiate(flame, transform.position, transform.rotation);
            disappearingWall.GetComponent<DisappearingWall>().disappear();
            dead = true;
            gameObject.SetActive(false);
        } else
        {
            StartCoroutine(flash(.25f));
        }
    }

    IEnumerator flash(float time)
    {
        for (float i = 0; i < 1; i += Time.deltaTime / time)
        {
            Color current = sprite.color;
            float alpha = current.a;
            if (i < .5f)
            {
                Color newColor = current;
                newColor.a = 0;
                sprite.color = Color.Lerp(current, newColor, i * 2f);
                yield return null;
            } else
            {
                Color newColor = current;
                newColor.a = 1;
                sprite.color = Color.Lerp(current, newColor, i * 2f);
                yield return null;
            }
        }
    }
}
