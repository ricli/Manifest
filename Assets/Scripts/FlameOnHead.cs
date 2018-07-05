using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameOnHead : MonoBehaviour {

	public GameObject flameOnHead;
	public GameObject playerHead;

	private Vector3 offset;

	void Awake () {
		offset = new Vector3 (0.03f, 0.45f, 0);
	}

	void Update () {
		Vector3 playerHeadTransform = playerHead.transform.position;
		Vector3 flameOnHeadTransform = flameOnHead.transform.position;

		flameOnHeadTransform.x = playerHeadTransform.x + offset.x;
		flameOnHeadTransform.y = playerHeadTransform.y + offset.y;
	
		flameOnHead.transform.position = flameOnHeadTransform;
	}
}
