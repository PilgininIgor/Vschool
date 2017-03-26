using UnityEngine;
using System.Collections;

public class TextHints : MonoBehaviour {
	
	void ShowHint(string message)
	{
		GetComponent<GUIText>().text = message;
		if(!GetComponent<GUIText>().enabled){ GetComponent<GUIText>().enabled = true; }
	}
}