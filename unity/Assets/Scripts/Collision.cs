using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour {

	public GameObject player;
	public bool movingForward = false;
	public bool movingBackward = false;
	public bool intersects = false;

	private Vector3 prevPos = new Vector3(0,0,0);
	private Vector3 currPos = new Vector3(0,0,0);
	private Vector3 movement = new Vector3(0,0,0);
	private bool firstmove = true;

	void Update() {
		if (movingBackward) {
			if (firstmove) {currPos = transform.position; firstmove = false;}
			prevPos = currPos;
			currPos = transform.position;
			movement = currPos - prevPos;
			if (intersects) { player.transform.position += movement*1.2f; }
		}
		else {
			if (!firstmove) firstmove = true;
		}	
	}

	void OnTriggerEnter(Collider other) { intersects = true; }
	void OnTriggerExit(Collider other) { intersects = false; }
}
