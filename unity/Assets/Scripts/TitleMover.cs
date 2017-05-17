using UnityEngine;
using System.Collections;
using System;

public class TitleMover : MonoBehaviour {
	public float scrollSpeed = 0.2f;
	public float stopTime = 5f;
	public int positionScroll = 1;
	public string title = "Закрыто";

	private GameObject tablo;
	private float width;
	private string currentTitle;
	private int position;
	private float currentTime;
	private float nextTime;
	private bool active;

	void Start ()
	{
		position = 0;
		currentTime = 0f;
		nextTime = 0f;
		tablo = transform.parent.Find ("Monitor/Tablo").gameObject;

		float wx = tablo.GetComponent<MeshFilter> ().mesh.bounds.size.x * tablo.transform.localScale.x;
		float wy = tablo.GetComponent<MeshFilter> ().mesh.bounds.size.y * tablo.transform.localScale.y;
		float wz = tablo.GetComponent<MeshFilter> ().mesh.bounds.size.z * tablo.transform.localScale.z;

		if (wx > wy && wx > wz) {
			width = wx;
		} else if (wy > wx && wy > wz) {
			width = wy;
		} else {
			width = wz;
		}

		//Проверка того, что заголовок не умещается в табло, и поэтому его надо скроллить
		int curPos = TestTitle (0);
		if (curPos >= title.Length) {
			GetComponent<TextMesh> ().text = title;
			active = false;
		} else {
			nextTime += stopTime;

			active = true;
		}
	}

	void Update ()
	{
		if (active) {
			currentTime += Time.deltaTime;
			if (currentTime > nextTime) {
				nextTime += scrollSpeed;
				position += positionScroll;
				if (position > title.Length + positionScroll * 5) {
					position = 0;
					nextTime += stopTime;
				}
				int curPos = TestTitle (position);

			}
		}
	}

	public void ChangeTitle(string newTitle) {
		title = newTitle;
		int curPos = TestTitle (0);
		if (curPos >= title.Length) {
			GetComponent<TextMesh> ().text = title;
			active = false;
		} else {
			position = 0;
			nextTime += stopTime;
			Debug.Log (GetComponent<TextMesh> ().text);

			active = true;
		}
	}

	int TestTitle (int curPos) {
		GetComponent<TextMesh>().text = "Закрыто";
		string d = "";
		float w = 0;
		if (GetComponent<MeshRenderer> ().bounds.size.x > GetComponent<MeshRenderer> ().bounds.size.y &&
		    GetComponent<MeshRenderer> ().bounds.size.x > GetComponent<MeshRenderer> ().bounds.size.z) {
			d = "x";
		} else if (GetComponent<MeshRenderer> ().bounds.size.y > GetComponent<MeshRenderer> ().bounds.size.x &&
		           GetComponent<MeshRenderer> ().bounds.size.y > GetComponent<MeshRenderer> ().bounds.size.z) {
			d = "y";
		} else {
			d = "z";
		}
		GetComponent<TextMesh> ().text = "";
		switch (d) {
		case "x":
			w = GetComponent<MeshRenderer> ().bounds.size.x;
			break;
		case "y":
			w = GetComponent<MeshRenderer> ().bounds.size.y;
			break;
		case "z":
			w = GetComponent<MeshRenderer> ().bounds.size.z;
			break;
		}
		while (w <= width) {
			if (curPos < title.Length)
				GetComponent<TextMesh> ().text += title [curPos];
			else
				GetComponent<TextMesh> ().text += " ";
			curPos++;
			switch (d) {
			case "x":
				w = GetComponent<MeshRenderer> ().bounds.size.x;
				break;
			case "y":
				w = GetComponent<MeshRenderer> ().bounds.size.y;
				break;
			case "z":
				w = GetComponent<MeshRenderer> ().bounds.size.z;
				break;
			}
		}
		return curPos;
	}

}
