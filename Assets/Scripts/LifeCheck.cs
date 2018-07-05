using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeCheck : MonoBehaviour {

    public PlayerGeneral player;

    public Text text;
	
	// Update is called once per frame
	void Update () {
		text.text = player.tries.ToString();
    }
}
