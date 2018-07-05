using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingWall : MonoBehaviour {
    MeshRenderer mesh;
    float fadeTime = 1f;

	// Use this for initialization
	void Start () {
        mesh = GetComponent<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void disappear()
    {
        StartCoroutine(fadeOut(gameObject, 5f));
    }

    private IEnumerator fadeOut(GameObject target, float time)
    {
        for(float i = 0f; i <= 1f; i += Time.deltaTime / time)
        {
            Color newColor = mesh.material.color;
            newColor.a = Mathf.Lerp(newColor.a, 0, i);
            mesh.material.color = newColor;
            yield return null;
        }
        Destroy(target);
    }
}
