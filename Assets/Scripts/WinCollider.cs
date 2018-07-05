using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCollider : MonoBehaviour {

    private LevelManager levelManager;

    // Triggers don't follow physics, they do something caused by the script instead
    void OnTriggerEnter2D(Collider2D trigger)
    {
        print("Trigger");
        levelManager = GameObject.FindObjectOfType<LevelManager>();
        levelManager.LoadLevel("Win");
    }
}
