using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNormal : MonoBehaviour {

	public int purifiedAmount;
	public int corruptionID;
	public bool firstTime = true;

    //State check
    public bool isGrounded;
    public int dirFacing;
    private bool controlsEnabled = true;

	private AudioSource music;
	public AudioClip steps;

    //Movement Utility Variables
    private Animator playerAnimator;
    private Rigidbody2D PlayerRigidbody;
    private float MovementInputX;
    private float MovementInputY;
    private float Speed = 4f;
	public Vector3 jump;
	public float jumpForce = 5.0f;
	public float knockbackTime = 0;

    private void Awake()
    {
        dirFacing = 1;
        playerAnimator = GetComponent<Animator>();
        PlayerRigidbody = GetComponent<Rigidbody2D>();
		jump = new Vector2(0, 420);
		music = GetComponent<AudioSource>();
		purifiedAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        controlsEnabled = GetComponentInParent<PlayerGeneral>().controlsEnabled;
        MovementInputX = Input.GetAxisRaw("Horizontal");
        MovementInputY = Input.GetAxisRaw("Vertical");
        if (MovementInputX != 0 && MovementInputX != dirFacing)
        {
            Vector3 temp = gameObject.transform.localScale;
            temp.x = -temp.x;
            gameObject.transform.localScale = temp;
            dirFacing = -dirFacing;
        }
    }

    private void FixedUpdate()
    {
		if (knockbackTime == 0) {
			if (isGrounded && controlsEnabled) {
				if (Input.GetKeyDown (KeyCode.W)) {
					Jump ();
				}
			}
            if (controlsEnabled)
            {
                Animate();
                Move();
            }
		} else {
			knockbackTime -= 1;
		}
    }

    void Animate()
    {
        if (isGrounded && MovementInputX != 0)
        {
            playerAnimator.SetInteger("walking", 1);
        } else
        {
            playerAnimator.SetInteger("walking", 0);
        }
    }

    //Move left or right.
    void Move()
    {
		PlayerRigidbody.velocity = new Vector2(MovementInputX * Speed, PlayerRigidbody.velocity.y);
    }

	void Jump()
	{
		PlayerRigidbody.velocity = new Vector2 (PlayerRigidbody.velocity.x, 0);
		PlayerRigidbody.AddForce (jump);
	}


    //Check if we're in air. If in air, should not be able to move left or right.
	private void OnTriggerStay2D(Collider2D collision)
	{
        if (collision.gameObject.tag == "Environment")
        {
            isGrounded = true;
        }
	}

	private void OnTriggerExit2D(Collider2D colllision)
	{
		isGrounded = false;
	}

	public void setID(int id) {
    	corruptionID = id;
    }

    public void playSteps() {
    	music.clip = steps;
    	music.volume = 0.2f;
    	music.Play();
    }
}
