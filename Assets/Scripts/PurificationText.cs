using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurificationText : MonoBehaviour {

	public PlayerNormal player;
	private Text text;

	// Use this for initialization
	void Awake () {
		text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		text.text = player.purifiedAmount.ToString();
	}
}
