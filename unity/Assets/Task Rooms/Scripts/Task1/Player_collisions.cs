using UnityEngine;
using System.Collections;

public class Player_collisions : MonoBehaviour {
	
	public GameObject button0;
	public GameObject button1;
	public GameObject button2;
	public GameObject button3;
	public GameObject button4;
	public GameObject button5;
	public GameObject button6;
	public GameObject button7;
	public GameObject button8;
	public GameObject button9;
	public GameObject buttonA;
	public GameObject buttonB;
	public GameObject buttonC;
	public GameObject buttonD;
	public GameObject buttonE;
	public GameObject buttonF;
	public GameObject buttonCheck;
	public GUIText textHints;
	RaycastHit hit;
	
	void Update () 
	{
		if (Physics.Raycast(transform.position, transform.forward, out hit, 4) && !GameObject.Find ("grab").GetComponent<Grab> ().IsGrabbed ())
		{
			if (hit.collider.gameObject == button0)
			{
				textHints.SendMessage("ShowHint", "Нажмите E");
				if (Input.GetKeyDown(KeyCode.E))
				{
					GameObject.Find("Cube_generator").GetComponent<Inst_cube>().GenerateCube("0");
				}		
			}
			else
			{
				textHints.SendMessage("ShowHint", "");
			}
			if (hit.collider.gameObject == button1)
			{
				textHints.SendMessage("ShowHint", "Нажмите E");
				if (Input.GetKeyDown(KeyCode.E))
				{
					GameObject.Find("Cube_generator").GetComponent<Inst_cube>().GenerateCube("1");
				}		
			}
			if (hit.collider.gameObject == button2)
			{
				textHints.SendMessage("ShowHint", "Нажмите E");
				if (Input.GetKeyDown(KeyCode.E))
				{
					GameObject.Find("Cube_generator").GetComponent<Inst_cube>().GenerateCube("2");
				}		
			}
			if (hit.collider.gameObject == button3)
			{
				textHints.SendMessage("ShowHint", "Нажмите E");
				if (Input.GetKeyDown(KeyCode.E))
				{
					GameObject.Find("Cube_generator").GetComponent<Inst_cube>().GenerateCube("3");
				}		
			}
			if (hit.collider.gameObject == button4)
			{
				textHints.SendMessage("ShowHint", "Нажмите E");
				if (Input.GetKeyDown(KeyCode.E))
				{
					GameObject.Find("Cube_generator").GetComponent<Inst_cube>().GenerateCube("4");
				}		
			}
			if (hit.collider.gameObject == button5)
			{
				textHints.SendMessage("ShowHint", "Нажмите E");
				if (Input.GetKeyDown(KeyCode.E))
				{
					GameObject.Find("Cube_generator").GetComponent<Inst_cube>().GenerateCube("5");
				}		
			}
			if (hit.collider.gameObject == button6)
			{
				textHints.SendMessage("ShowHint", "Нажмите E");
				if (Input.GetKeyDown(KeyCode.E))
				{
					GameObject.Find("Cube_generator").GetComponent<Inst_cube>().GenerateCube("6");
				}		
			}
			if (hit.collider.gameObject == button7)
			{
				textHints.SendMessage("ShowHint", "Нажмите E");
				if (Input.GetKeyDown(KeyCode.E))
				{
					GameObject.Find("Cube_generator").GetComponent<Inst_cube>().GenerateCube("7");
				}		
			}
			if (hit.collider.gameObject == button8)
			{
				textHints.SendMessage("ShowHint", "Нажмите E");
				if (Input.GetKeyDown(KeyCode.E))
				{
					GameObject.Find("Cube_generator").GetComponent<Inst_cube>().GenerateCube("8");
				}		
			}
			if (hit.collider.gameObject == button9)
			{
				textHints.SendMessage("ShowHint", "Нажмите E");
				if (Input.GetKeyDown(KeyCode.E))
				{
					GameObject.Find("Cube_generator").GetComponent<Inst_cube>().GenerateCube("9");
				}		
			}
			if (hit.collider.gameObject == buttonA)
			{
				textHints.SendMessage("ShowHint", "Нажмите E");
				if (Input.GetKeyDown(KeyCode.E))
				{
					GameObject.Find("Cube_generator").GetComponent<Inst_cube>().GenerateCube("A");
				}		
			}
			if (hit.collider.gameObject == buttonB)
			{
				textHints.SendMessage("ShowHint", "Нажмите E");
				if (Input.GetKeyDown(KeyCode.E))
				{
					GameObject.Find("Cube_generator").GetComponent<Inst_cube>().GenerateCube("B");
				}		
			}
			if (hit.collider.gameObject == buttonC)
			{
				textHints.SendMessage("ShowHint", "Нажмите E");
				if (Input.GetKeyDown(KeyCode.E))
				{
					GameObject.Find("Cube_generator").GetComponent<Inst_cube>().GenerateCube("C");
				}		
			}
			if (hit.collider.gameObject == buttonD)
			{
				textHints.SendMessage("ShowHint", "Нажмите E");
				if (Input.GetKeyDown(KeyCode.E))
				{
					GameObject.Find("Cube_generator").GetComponent<Inst_cube>().GenerateCube("D");
				}		
			}
			if (hit.collider.gameObject == buttonE)
			{
				textHints.SendMessage("ShowHint", "Нажмите E");
				if (Input.GetKeyDown(KeyCode.E))
				{
					GameObject.Find("Cube_generator").GetComponent<Inst_cube>().GenerateCube("E");
				}		
			}
			if (hit.collider.gameObject == buttonF)
			{
				textHints.SendMessage("ShowHint", "Нажмите E");
				if (Input.GetKeyDown(KeyCode.E))
				{
					GameObject.Find("Cube_generator").GetComponent<Inst_cube>().GenerateCube("F");
				}		
			}
			if (hit.collider.gameObject == buttonCheck)
			{
				if (!GameObject.Find("buttonOK").GetComponent<Check_result>().TaskSolved())
				{
					textHints.SendMessage("ShowHint", "Нажмите E");
				}
				if (Input.GetKeyDown(KeyCode.E))
				{
					GameObject.Find("buttonOK").GetComponent<Check_result>().Check();
				}		
			}
			if (hit.rigidbody)
			{
				textHints.SendMessage("ShowHint", "Нажмите ПКМ");
			}
		}
		else
		{
			textHints.SendMessage("ShowHint", "");
		}
	}
}
