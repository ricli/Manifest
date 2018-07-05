using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour {

	public Transform[] backgrounds;			// Array of all back/foregrounds to be parallaxed
	private float[] parallaxScales;			// Proportion of the camera's movement to move the backgrounds by
	public float smoothing = 1f;			// How smooth the parallax is going to be. Set this above 0!
	public Renderer myRenderer;

	private Transform cam;					// reference to the main cameras transform 
	private Vector3 previousCamPos;			// the position of the camera in the previous frame

	// Is called before Start()
	void Awake () {
		cam = Camera.main.transform;
	}

	// Use this for initialization
	void Start () {
		previousCamPos = cam.position;

		parallaxScales = new float[backgrounds.Length];

		for (int i = 0; i < backgrounds.Length; i++) {
			parallaxScales [i] = backgrounds [i].position.z * -1;
		}
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < backgrounds.Length; i++) {
			// difference between the movement in x multiplied by our parallax scale
			float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];

			// apply parallax to each layer
			float backgroundTargetPosX = backgrounds [i].position.x + parallax;
			Vector2 offset = new Vector2(backgroundTargetPosX * .025f, 0);
			myRenderer.material.mainTextureOffset = offset;
		}

		previousCamPos = cam.position;
	}
}
