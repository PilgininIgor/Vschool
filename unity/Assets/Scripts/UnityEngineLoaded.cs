using UnityEngine;
using System.Collections;

public class UnityEngineLoaded : MonoBehaviour {

	void Start () {
		Application.ExternalCall("UnityLoaded");
	}
}
