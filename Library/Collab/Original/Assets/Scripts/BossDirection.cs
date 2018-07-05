using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDirection : MonoBehaviour {

    private int dir;
    // Use this for initialization
    void Start()
    {

        dir = GetComponentInParent<BossMovement>().dir;

    }

    // Update is called once per frame
    void Update()
    {
        GetComponentInParent<BossMovement>().dir = dir;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Environment")
        {
            dir = -dir;
        }
    }
}
