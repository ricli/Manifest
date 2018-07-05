using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDirection : MonoBehaviour
{

    private int dir;
    public GameObject player;

    private Transform playerTransform;
    public PlayerNormal playerNormal;

    // Use this for initialization
    void Start()
    {
        playerTransform = player.transform;
        dir = GetComponentInParent<EnemyMovement>().dir;

    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform.position.y > -23)
        {
            dir = 1;
        }
        else if (playerNormal.isGrounded && transform.position.x - playerTransform.position.x < -2)
        {
            dir = 1;
        }
        else if (playerNormal.isGrounded && transform.position.x - playerTransform.position.x > 2)
        {
            dir = -1;
        }
        GetComponentInParent<EnemyMovement>().dir = dir;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Environment")
        {
            dir = -dir;
        }
    }
}
