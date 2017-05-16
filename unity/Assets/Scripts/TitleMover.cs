using UnityEngine;
using System.Collections;
using System;

public class TitleMover : MonoBehaviour {
	public float scrollSpeed = 0.2f;
	public float stopTime = 5f;
	public int positionScroll = 1;
	public string title = "";

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
		width = tablo.GetComponent<MeshFilter>().mesh.bounds.size.x * tablo.transform.localScale.x;

		//Проверка того, что заголовок не умещается в табло, и поэтому его надо скроллить
		int curPos = DrawTitle (0);
		if (curPos >= title.Length) {
			GetComponent<TextMesh> ().text = title;
			active = false;
		}
		else
			active = true;
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
				DrawTitle (position);
			}
		}
	}

	public void ChangeTitle(string newTitle) {
		title = newTitle;
		int curPos = DrawTitle (0);
		if (curPos >= title.Length) {
			GetComponent<TextMesh> ().text = title;
			active = false;
		} else {
			position = 0;
			active = true;
		}
	}

	int DrawTitle (int startPos) {
		int curPos = startPos;
		GetComponent<TextMesh>().text = "";
		while (GetComponent<MeshRenderer> ().bounds.size.x <= width) {
			if (curPos < title.Length)
				GetComponent<TextMesh> ().text += title [curPos];
			else
				GetComponent<TextMesh> ().text += " ";
			curPos++;
		}
		return curPos;
	}

}
