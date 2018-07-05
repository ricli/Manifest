using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonBoss : MonoBehaviour {

    public GameObject boss;
	public InstructionText instructionText;
	bool firstTime = true;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (!boss.GetComponent<EnemyMovement>().dead)
            {
                boss.SetActive(true);
                instructionText.gameObject.SetActive(true);
                if (firstTime)
                {
                    instructionText.changeText("Press E to attack.");
                    firstTime = false;
                }
            }
        }
    }
}
