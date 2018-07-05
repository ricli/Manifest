using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Corruption : MonoBehaviour {

    public GameObject boss;
    public Sprite corruption;
    public Sprite purify;
	public GameObject dialogue;
	public Slider purificationBar;
	public Slider morphBar;

    private SpriteRenderer renderer;
    private Text text;
    private float holder;
    private bool visited;

    void Awake() {
		purificationBar.value = purificationBar.maxValue;
    	renderer = GetComponent<SpriteRenderer>();
		text = dialogue.GetComponentInChildren<Text>();
		visited = true;
    }

	// Update is called once per frame
	void Update () {
		if (visited) {
			if (Input.GetKey(KeyCode.J)) {
			purificationBar.value -= 2f * Time.deltaTime;
			} else {
			purificationBar.value += 2f * Time.deltaTime;
			}

			if (purificationBar.value <= 0) {
				renderer.sprite = purify;
				visited = false;
				text.text = "Corruption purified!";
			}
		}
//		if (!visited) {
//			purificationBar.value = purificationBar.maxValue;
//		}
// right now purifies all the corruption
	}

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
        	if (visited) {
        		text.text = "Hold J to purify the corruption.";
        	}
			morphBar.gameObject.SetActive(false);
			purificationBar.gameObject.SetActive(true);
//            GetComponentInParent<ObjectiveCounter>().numCorruptionsLeft -= 1;
//            Destroy(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
   		morphBar.gameObject.SetActive(true);
		purificationBar.gameObject.SetActive(false);
    }
}
