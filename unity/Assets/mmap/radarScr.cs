using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class radarScr : MonoBehaviour {

public List<GameObject> Enemy = new List<GameObject>();

public Texture2D enemyIcon;
public Texture2D meIcon;
public Texture2D compass;
public Texture2D camView;
public Texture2D minimapFon;
public Texture2D enemyStanc;
	
public Transform character;
public Transform mainCam;

public float distance;
	
	void Update()
	{
		transform.eulerAngles = new Vector3(90f, character.eulerAngles.y, 0f);
	}
	
    void OnGUI()
    {
		camera.rect = new Rect(0,0, 200f/Screen.width, 200f/Screen.height);
		GUI.DrawTexture( new Rect(10, Screen.height-190, 178, 178), minimapFon );
		
		
		foreach (GameObject e in Enemy)
		{
			if (e!=null)
			{
				transform.position = new Vector3(character.position.x, e.transform.position.y+distance, character.position.z);
				if (Vector3.Distance(transform.position, e.transform.position) < distance+distance/12.5f)
				{
					if (Vector3.Dot(transform.forward, e.transform.position-transform.position) >= 0)
					{
						Vector3 screenPos = camera.WorldToScreenPoint(e.transform.position);
						if (new Rect(0, 0, Screen.width, Screen.height).Contains(screenPos))
						{
							GUI.DrawTexture( new Rect(screenPos.x-7, camera.GetScreenHeight()-screenPos.y-7, 14, 14), enemyIcon );
						}
					}
				}
				else
				{
					Matrix4x4 matrr = GUI.matrix;
					Vector3 olRot = transform.eulerAngles;
					if (character!=null)
					{
						transform.LookAt(e.transform.position);
						GUIUtility.RotateAroundPivot(transform.eulerAngles.y - character.eulerAngles.y, new Vector2 (100f, Screen.height-100f));
					}
					GUI.DrawTexture( new Rect (10, Screen.height-190, 178, 178), enemyStanc);
					transform.eulerAngles = olRot;
					GUI.matrix = matrr;
				}
			}
		}
		
		Matrix4x4 matr = GUI.matrix;
		GUIUtility.RotateAroundPivot(mainCam.eulerAngles.y-character.eulerAngles.y , new Vector2 (100f, Screen.height-100f));
		GUI.DrawTexture( new Rect (10, Screen.height-190, 178, 178), camView);
		GUI.matrix = matr;
			
			
		GUI.DrawTexture( new Rect(10, Screen.height-190, 178, 178), meIcon );
		GUIUtility.RotateAroundPivot(-transform.eulerAngles.y, new Vector2 (99.2f, Screen.height-101.3f));
		GUI.DrawTexture( new Rect (10, Screen.height-190, 178, 178), compass);
    }
}
