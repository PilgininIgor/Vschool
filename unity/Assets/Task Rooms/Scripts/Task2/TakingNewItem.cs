using UnityEngine;
using System.Collections;

public class TakingNewItem : MonoBehaviour {
	
	RaycastHit hit;
	public GUIText textHints;
	public GameObject boxAND;
	public GameObject boxOR;
	public GameObject boxIMPL;
	public GameObject boxEQUIV;
	public GameObject box1;
	public GameObject box0;
	public GameObject buttonCheck;
	
	void Update () 
	{
		if (Physics.Raycast (transform.position, transform.forward, out hit, 4)  && !buttonCheck.GetComponent<CheckResult2>().TaskSolved()) 
		{
			string itemType = GameObject.Find ("hand").GetComponent<InstItem> ().CurrentItemType();
			if (hit.collider.gameObject == boxAND) 
			{
				textHints.SendMessage ("ShowHint", "Нажмите E");
				if (Input.GetKeyDown (KeyCode.E)) 
				{
					GameObject.Find ("hand").GetComponent<InstItem> ().GenerateItem ("AND");
				}		
			}
			else 
			{
				textHints.SendMessage ("ShowHint", "");
			}
			if (hit.collider.gameObject == boxOR) 
			{
				textHints.SendMessage ("ShowHint", "Нажмите E");
				if (Input.GetKeyDown (KeyCode.E)) 
				{
					GameObject.Find ("hand").GetComponent<InstItem> ().GenerateItem ("OR");
				}		
			}
			if (hit.collider.gameObject == boxIMPL) 
			{
				textHints.SendMessage ("ShowHint", "Нажмите E");
				if (Input.GetKeyDown (KeyCode.E)) 
				{
					GameObject.Find ("hand").GetComponent<InstItem> ().GenerateItem ("IMPL");
				}		
			}
			if (hit.collider.gameObject == boxEQUIV) 
			{
				textHints.SendMessage ("ShowHint", "Нажмите E");
				if (Input.GetKeyDown (KeyCode.E)) 
				{
					GameObject.Find ("hand").GetComponent<InstItem> ().GenerateItem ("EQUIV");
				}		
			}
			if (hit.collider.gameObject == box1) 
			{
				textHints.SendMessage ("ShowHint", "Нажмите E");
				if (Input.GetKeyDown (KeyCode.E)) 
				{
					GameObject.Find ("hand").GetComponent<InstItem> ().GenerateItem ("1");
				}		
			}
			if (hit.collider.gameObject == box0) 
			{
				textHints.SendMessage ("ShowHint", "Нажмите E");
				if (Input.GetKeyDown (KeyCode.E)) 
				{
					GameObject.Find ("hand").GetComponent<InstItem> ().GenerateItem ("0");
				}		
			}
			if (hit.collider.gameObject == buttonCheck) 
			{
				textHints.SendMessage ("ShowHint", "Нажмите E");
				if (Input.GetKeyDown (KeyCode.E)) 
				{
					buttonCheck.GetComponent<CheckResult2> ().Check ();
				}		
			}
			if ((hit.collider.gameObject.tag == "BoolPanel" && (itemType == "1" || itemType == "0" || itemType == "")) ||
			    (hit.collider.gameObject.tag == "OperPanel" && (itemType == "AND" || itemType == "OR" || itemType == "IMPL" || itemType == "EQUIV" || itemType == "")))
			{
			    textHints.SendMessage ("ShowHint", "Нажмите E");
				if (Input.GetKeyDown (KeyCode.E)) 
				{
					GameObject.Find ("insert").GetComponent<InsertItem> ().Insert (hit.collider.gameObject);
				}
			}
		} 
		else 
		{
			textHints.SendMessage ("ShowHint", "");
		}
	}
}