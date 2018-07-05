using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ExitMenu : MonoBehaviour {

	public Text restartLevel;
	public Text returnStart;
	bool display = false;

	// Use this for initialization
	void Awake () {
		restartLevel.gameObject.SetActive(false);
		returnStart.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			display = !display;
			restartLevel.gameObject.SetActive(display);
			returnStart.gameObject.SetActive(display);
		}
	}
}
