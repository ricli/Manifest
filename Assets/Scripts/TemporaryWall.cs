using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryWall : MonoBehaviour {
    Transform wallTransform;
    float speed = 2f;


	// Use this for initialization
	void Start () {
        wallTransform = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void moveWall()
    {
        Vector3 pos = wallTransform.position;
        wallTransform.position = Vector3.MoveTowards(pos, new Vector3(pos.x, pos.y - 6f, pos.z), speed * Time.deltaTime);
    }

    public void moveBack()
    {
        Vector3 pos = wallTransform.position;
        wallTransform.position = Vector3.MoveTowards(pos, new Vector3(pos.x, pos.y + 0.06f, pos.z), speed * Time.deltaTime);
    }
}
