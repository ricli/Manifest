using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int Flame;
    private int dirFacing = 1;
    private int dirY = 0;
    private float morphDuration;
    //private float morphCD = 0;

    //Player colliders
    private BoxCollider2D PlayerBody;

    public Sprite dude;
    public Sprite morph;
    private SpriteRenderer PlayerSR;

    //Slider to show duration of Morph for now
    public Slider morphBar;

    //Last Checkpoint
    public GameObject LastHearth;

    //State check
    private bool isMorphed;
    private bool isGrounded;

    //Movement Utility Variables
    private Rigidbody2D PlayerRigidbody;
    private float MovementInputX;
    private float MovementInputY;
    private float Speed = 5f;

    private void Awake()
    {
        morphDuration = (float)Flame;
        PlayerSR = GetComponent<SpriteRenderer>();
        PlayerBody = GetComponent<BoxCollider2D>();
        PlayerRigidbody = GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MovementInputX = Input.GetAxisRaw("Horizontal");
        MovementInputY = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown(KeyCode.Space) /* && morphCD == 0 */)
        {
            Morph();
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Revert();
        }
        if (isMorphed)
        {
            morphDuration -= 1.5f * Time.deltaTime;
			morphBar.value = morphDuration;
            if (morphDuration <= 0)
            {
                Revert();
            }
        }
        if (isGrounded)
        {
            morphDuration += (Time.deltaTime * 2.5f);
			morphBar.value = morphDuration;
            if (morphDuration > (float) Flame)
            {
                morphDuration = (float) Flame;
            }
        }
    }

    private void FixedUpdate()
    {
        if (isMorphed)
        {
			if (MovementInputX > 0)
			{
				PlayerSR.flipX = false;
				GetComponentInChildren<CircleCollider2D> ().offset = new Vector2 (5, 0);
				dirFacing = 1;
                Debug.Log(MovementInputY);
                if (MovementInputY > 0)
                {
                    transform.eulerAngles = new Vector3(0, 0, 45);
                    dirY = 1;
                }
                else if (MovementInputY < 0)
                {
                    transform.eulerAngles = new Vector3(0, 0, -45);
                    dirY = -1;
                } else
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    dirY = 0;
                }
            }
			if (MovementInputX < 0)
			{
				PlayerSR.flipX = true;
				GetComponentInChildren<CircleCollider2D> ().offset = new Vector2 (-5, 0);
				dirFacing = -1;
                Debug.Log(MovementInputY);
                if (MovementInputY > 0)
                {
                    transform.eulerAngles = new Vector3(0, 0, -45);
                    dirY = 1;
                } else if (MovementInputY < 0) {
                    transform.eulerAngles = new Vector3(0, 0, 45);
                    dirY = -1;
                } else
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    dirY = 0;
                }
            }
            if (MovementInputX == 0 && MovementInputY > 0)
            {
                if (dirFacing == 1)
                {
                    transform.eulerAngles = new Vector3(0, 0, 90);
                } else
                {
                    transform.eulerAngles = new Vector3(0, 0, -90);
                }
                dirY = 1;
            }
            if (MovementInputX == 0 && MovementInputY < 0)
            {
                if (dirFacing == 1)
                {
                    transform.eulerAngles = new Vector3(0, 0, -90);
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 0, 90);
                }
                dirY = 1;
            }
            if (MovementInputY == 0) {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    dirY = 0;
                }
            MoveMorph();
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            //morphCD -= Time.deltaTime;
            if (isGrounded)
            {
                if (MovementInputX > 0 && dirFacing < 0)
                {
                    PlayerSR.flipX = false;
                    dirFacing = 1;
                }
                if (MovementInputX < 0 && dirFacing > 0)
                {
                    PlayerSR.flipX = true;
                    dirFacing = -1;
                }
                Move();
            }
        }
    }

    //Move left or right.
    void Move()
    {
        Vector2 movement = transform.right * MovementInputX * Speed * Time.deltaTime;
        //PlayerRigidbody.velocity = Speed * movement;
        PlayerRigidbody.MovePosition(PlayerRigidbody.position + movement);
    }

    //Flight during morph
    void MoveMorph()
    {
        Vector2 movement = new Vector2(MovementInputX, MovementInputY);
        movement *= Time.deltaTime * Speed;
        PlayerRigidbody.MovePosition(PlayerRigidbody.position + movement);
    }

    //Activate Morph
    void Morph()
    {
        isMorphed = true;
        PlayerSR.sprite = morph;
        PlayerRigidbody.gravityScale = 0;
        PlayerBody.enabled = false;
        GetComponentInChildren<CircleCollider2D>().enabled = true;

        //morphCD = 5f;
    }

    void Revert()
    {
        isMorphed = false;
        PlayerSR.sprite = dude;
        PlayerRigidbody.gravityScale = 2;
        PlayerBody.enabled = true;
        GetComponentInChildren<CircleCollider2D>().enabled = false;
    }

    //Check if we're in air. If in air, should not be able to move left or right.
    private void OnCollisionStay2D(Collision2D collision)
    {
        isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }

    //Reduce flame by the amount of damage received. If dead, respawn.
    //This method should be called through the script of the source of the damage.
    public void TakeDamage(int damage)
    {
        Flame -= damage;
        if(Flame <= 0)
        {
            Respawn();
        }
    }

    //Add to flame total by picking up flames
    public void pickUpFlame()
    {
        Flame += 1;
        morphDuration = Flame;
    }

    //Go back to the last Hearth Tree
    void Respawn()
    {
        
    }
}
