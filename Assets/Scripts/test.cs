using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {

	private float MovementInputX;
    private float MovementInputY;
    public float Speed = 0.5f;
    private bool walking;
    Animator animator;


    void Awake ( ) {

    	animator = GetComponent<Animator>();
    	walking = false;
    }

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		MovementInputX = Input.GetAxisRaw("Horizontal");
        MovementInputY = Input.GetAxisRaw("Vertical");
		if (MovementInputX != 0) {
       		move();
       		walking = true;
        } else {
        	walking = false;
        }
		animate();

	}

	void animate ( ) {
		if (walking) {
			animator.SetInteger("walking", 1);
		} else {
			animator.SetInteger("walking", 0);
			walking = false;
		}
	}
	void move()
    {
        transform.position += transform.right * MovementInputX * Speed * Time.deltaTime;
    }

}
