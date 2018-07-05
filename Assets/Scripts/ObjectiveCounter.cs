using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveCounter : MonoBehaviour {

    public int numCorruptionsLeft;

	// Use this for initialization
	void Start () {
        numCorruptionsLeft = transform.childCount;
        Debug.Log(numCorruptionsLeft);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
