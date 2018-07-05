using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryWallTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GetComponentInParent<TemporaryWall>().moveWall();
            StartCoroutine(moveBack());
        }
    }

    IEnumerator moveBack()
    {
        yield return new WaitForSeconds(5f);
        for (float t = 0; t < 0.0165f; t += Time.deltaTime) {
            GetComponentInParent<TemporaryWall>().moveBack();
            yield return new WaitForSeconds(0.05f);
        }
    }
}
