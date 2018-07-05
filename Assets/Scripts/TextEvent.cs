using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextEvent : MonoBehaviour {

    public string text;
    public GameObject dialogue;

    private bool seen = false;
    private TextController controller;

	// Use this for initialization
	void Start () {
        controller = dialogue.GetComponentInChildren<TextController>();
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !seen)
        {
            seen = true;
            controller.displayMessage(text);
        }
    }
}
