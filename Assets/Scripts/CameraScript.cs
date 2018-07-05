using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public Transform player;
    public BoxCollider2D leftWall;
    public BoxCollider2D rightWall;
    public BoxCollider2D topWall;
    public BoxCollider2D bottomWall;

    //changed maxOffset to 1.5 instead of 2.5
    private float maxOffset = 1.5f;
    private float yOffset;
    private float width;
    private float height;

    // Use this for initialization
    void Start () {
        yOffset = maxOffset;
        width = GetComponentInChildren<Camera>().scaledPixelWidth;
        height = GetComponentInChildren<Camera>().scaledPixelHeight;

        transform.position = new Vector3(player.position.x, player.position.y + yOffset);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        //Move();
        transform.position = transform.position = new Vector3(player.position.x, Mathf.Lerp(transform.position.y, player.position.y, 5f));
	}

    private void Move()
    {
        if (player.position.y < transform.position.y)
        {
            yOffset = transform.position.y - player.position.y;
        }
        yOffset = Mathf.Min(yOffset, maxOffset);
        transform.position = new Vector3(player.position.x, player.position.y + yOffset);
    }
}
