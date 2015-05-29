using UnityEngine;
using System.Collections;

public class TextHints : MonoBehaviour {
	
	void ShowHint(string message)
	{
		guiText.text = message;
		if(!guiText.enabled){ guiText.enabled = true; }
	}
}