using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CorruptionPoint : MonoBehaviour {

	public InstructionText instructionText;
	public PlayerNormal player;
	private SpriteRenderer renderer;
	public float fadeOutTime = 3.0f;

    public Sprite corruption;
    public Sprite purify;
	public int id;
	public AudioSource music;
	public AudioClip twinkle;
	public AudioClip channel;

	public GameObject boss;
	public GameObject dialogue;
	public Slider purificationBar;
	public Slider morphBar;

    private Text text;
    private float holder;
    private bool purified;
    private bool inRange;

	void Awake() {
    	renderer = GetComponent<SpriteRenderer>();
		purificationBar.value = purificationBar.maxValue;
		text = dialogue.GetComponentInChildren<Text>();
		purified = false;
		inRange = false;
		purificationBar.gameObject.SetActive(false);
		music = GetComponent<AudioSource>();
		instructionText.gameObject.SetActive(false);
	}

	void Update () {
		if (purified == false) {
			if (Input.GetKey(KeyCode.J)) {
				purificationBar.value -= 2f * Time.deltaTime;

			} else {
				purificationBar.value += 2f * Time.deltaTime;
			}

			if (purificationBar.value <= 0) {
				if (player.corruptionID == id && inRange) {
         			foreach (Transform child in this.gameObject.transform) {
             			child.gameObject.SetActive(true);
         			}
         			renderer.sprite = purify;
					purified = true;
					player.purifiedAmount += 1;
					instructionText.changeText("Corruption purified!");
					morphBar.gameObject.SetActive(false);
					music.clip = twinkle;
					music.Play();
					morphBar.gameObject.SetActive(true);
					purificationBar.gameObject.SetActive(false);
					player.firstTime = false;
				}
			}
		}
	}

	public int getID() {
		return id;
	}

	private void OnTriggerEnter2D(Collider2D other) {
		player.setID(id);
        if (other.tag == "Player" && purified == false) {
			inRange = true;
			instructionText.gameObject.SetActive(true);
			instructionText.changeText("Press J to purify the corruption.");
			morphBar.gameObject.SetActive(false);
			purificationBar.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
    	inRange = false;
		if (purified == false) {
			instructionText.gameObject.SetActive(false);
   			morphBar.gameObject.SetActive(true);
			purificationBar.gameObject.SetActive(false);
		}
    }

}
