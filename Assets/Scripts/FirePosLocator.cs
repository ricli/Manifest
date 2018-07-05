using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePosLocator : MonoBehaviour {

    public GameObject boss;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!boss.GetComponent<EnemyMovement>().dead)
        {
            transform.position = boss.transform.position;
        }
	}
}
