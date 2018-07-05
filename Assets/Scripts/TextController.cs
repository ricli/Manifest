using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour {

    private Queue<string> messageQueue = new Queue<string>();
    private string message;
    private bool displaying = false;
    Text textBox;

	// Use this for initialization
	void Start () {
        textBox = GetComponent<Text>();

        message = "Ah, you're awake. Good.";
        displayMessage(message);
	}
	
	// Update is called once per frame
	void Update () {
		if (messageQueue.Count > 0 && !displaying)
        {
            displayMessage(messageQueue.Dequeue());
        }
	}

    public void displayMessage (string text)
    {
        if (displaying)
        {
            messageQueue.Enqueue(text);
        }
        else
        {
            displaying = true;
            message = text;
            StartCoroutine(AnimateText(message));
        }
    }

    private IEnumerator AnimateText(string strComplete)
    {
        int i = 0;
        string str = "";
        while (i < strComplete.Length)
        {
            str += strComplete[i++];
            textBox.text = str;
            yield return new WaitForSeconds(0.01F);
        }
        yield return new WaitForSeconds(1F);
        displaying = false;
        yield return new WaitForSeconds(2F);
        textBox.text = "";
    }
}
