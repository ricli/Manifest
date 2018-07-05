using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	// Loads the level
	public void LoadLevel(string name) {
		Debug.Log ("Level load requested for: " + name);
		SceneManager.LoadScene(name);
	}
	
	public void QuitRequest() {
		Debug.Log("Quit load requested.");
		Application.Quit();
	}

}
