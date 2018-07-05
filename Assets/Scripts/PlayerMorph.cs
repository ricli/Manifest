using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMorph : MonoBehaviour {
	private PlayerGeneral pg;

	//Music
	private AudioSource music;
	public AudioClip morph;

    //Particle effect variables
	public ParticleSystem circle;
	public ParticleSystem core;
 	public ParticleSystem particles;
 	public ParticleSystem smoke;
	public ParticleSystem tail;
	public ParticleSystem tail2;
	public ParticleSystem tail3;

	private int maxTailParticles = 3000;
	ParticleSystem.EmissionModule emissionModule;
	ParticleSystem.EmissionModule emissionModule2;
	ParticleSystem.EmissionModule emissionModule3;

    //Movement variables
	private float MovementInputX;
    private float MovementInputY;
	private float Speed = 6f;
	private Rigidbody2D PlayerRigidbody;

    private bool controlsEnabled = true;


	// Use this for initialization
	void Start () {
		music = GetComponent<AudioSource>();
		pg = GetComponentInParent<PlayerGeneral> ();
		PlayerRigidbody = GetComponent<Rigidbody2D>();
		emissionModule = tail.emission;
		emissionModule2 = tail2.emission;
		emissionModule3 = tail3.emission;
		activate();
	}

    // Update is called once per frame
    void Update()
    {
        controlsEnabled = pg.controlsEnabled;
        MovementInputX = Input.GetAxisRaw("Horizontal");
        MovementInputY = Input.GetAxisRaw("Vertical");

        emissionModule.rate = maxTailParticles * ((pg.currentHealth - 2) / pg.maxHealth);
        emissionModule2.rate = maxTailParticles * ((pg.currentHealth - 2) / pg.maxHealth);
        emissionModule3.rate = maxTailParticles * ((pg.currentHealth - 2) / pg.maxHealth);

        if (controlsEnabled) {
            moveMorph();
        }
	}

	void moveMorph() {
		Vector2 movement = new Vector2(MovementInputX, MovementInputY);
		movement *= Time.deltaTime * Speed;
		PlayerRigidbody.velocity = new Vector2(MovementInputX * Speed, MovementInputY * Speed);
		//PlayerRigidbody.MovePosition(PlayerRigidbody.position + movement);

    }

	void activate () {
		music.clip = morph;
    	music.Play();
		circle.Play();
		core.Play();
		particles.Play();
		smoke.Play();
		tail.Play ();
	}

	void deactivate () {
		circle.Stop();
		core.Stop();
		particles.Stop();
		smoke.Stop();
		tail.Stop ();
	}
}
