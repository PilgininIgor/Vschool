using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using JsonFx.Json;
public class Game2_Start : MonoBehaviour {
	
	bool gameStarted = false;
	public Texture textureAND;
	public Texture textureOR;
	public Texture textureIMPL;
	public Texture textureEQUIV;
	public Texture texture1;
	public Texture texture0;
	public Texture textureBlank;
	public GameObject panel;
	public GameObject board;
	public GameObject link;
    public GameObject[] links;

	void OnTriggerEnter (Collider other)
	{
		if (other.GetComponent<Collider>().tag == "Player" && !gameStarted)
		{
			gameStarted = true;

			/*
            c - conjunction
            d - disjunction
            i - implication
            e - equivalence
            b - missing bool digit (1 or 0)
            o - missing operation
            1 - bool 1 (true)
            0 - bool 0 (false)
            */

            var parameters = new Dictionary<string, string>();
            parameters["id"] = Global.content.id;

			var httpConnector = GameObject.Find("TriggerStart").GetComponent<HttpConnector>();
			httpConnector.Post(HttpConnector.ServerUrl + HttpConnector.GetTask2Url, parameters, www =>
			{
				StartTask2(www.text);
				},
				w=>{});
		}
	}


	float GetAngleBetweenObjects(float x1, float y1, float x2, float y2) 
	{ 
		float kx,ky; 
		float t, a;
		kx=x2-x1; 
		ky=y2-y1; 
		if(kx==0)kx=0.00001f; 
		t=ky/kx; if(t<0)t=t*-1;
		
		a=(float)(180*Math.Atan((float)t)/Math.PI);
		
		if((kx<=0) && (ky>=0))a=180-a; else 
		if((kx<=0) && (ky<=0))a=180+a; else 
		if((kx>=0) && (ky<=0))a=359.99999f-a;
		
		return a; 
	}

	void StartTask2(string task)
	{
        links = new GameObject[15];
        for (int i = 0; i < links.Length; i++)
        {
            links[i] = null;
        }

		Vector3[][] coordinates = new Vector3[4][];
		coordinates[0] = new Vector3[1] {new Vector3(0.24f,0,7)};
		coordinates[1] = new Vector3[2] {new Vector3(0.02f,0.2f,7), new Vector3(0.02f,-0.2f,7)};
		coordinates[2] = new Vector3[4] {new Vector3(-0.2f,0.36f,7), new Vector3(-0.2f,0.12f,7), new Vector3(-0.2f,-0.12f,7), new Vector3(-0.2f,-0.36f,7)};
		coordinates[3] = new Vector3[8] {new Vector3(-0.42f,0.42f,7), new Vector3(-0.42f,0.3f,7), new Vector3(-0.42f,0.18f,7), new Vector3(-0.42f,0.06f,7), new Vector3(-0.42f,-0.06f,7), new Vector3(-0.42f,-0.18f,7), new Vector3(-0.42f,-0.3f,7), new Vector3(-0.42f,-0.42f,7)};
		
		int[] heights = new int[4]{0,0,0,0};
		GameObject[] lastObject = new GameObject[4]{null,null,null,null};
		int layer = 0, k = 0;
		
		GameObject linkObj = Instantiate (link, transform.position, transform.rotation) as GameObject;
		linkObj.name = "Link";
		linkObj.transform.parent = board.transform;
		linkObj.transform.localPosition = new Vector3(0.35f,0,-0.09f);

        int j = 1;
        links[0] = linkObj;
		
		for (int i=0; i<task.Length; i++)
		{
			GameObject panelObj = null;
			if (task[i] == '(')
			{
				layer++;
			}
			if (task[i] == ')')
			{
				layer--;
			}
			if (task[i] == 'c')
			{
				panelObj = Instantiate (panel, transform.position, transform.rotation) as GameObject;
				panelObj.name = "Panel";
				panelObj.transform.parent = board.transform;
				panelObj.transform.localPosition = coordinates[layer][heights[layer]];
				panelObj.transform.Rotate(new Vector3(0,180,0));
				panelObj.GetComponent<Renderer>().material.mainTexture = textureAND;
				heights[layer]++;
				lastObject[layer] = panelObj;
			}
			if (task[i] == 'd')
			{
				panelObj = Instantiate (panel, transform.position, transform.rotation) as GameObject;
				panelObj.name = "Panel";
				panelObj.transform.parent = board.transform;
				panelObj.transform.localPosition = coordinates[layer][heights[layer]];
				panelObj.transform.Rotate(new Vector3(0,180,0));
				panelObj.GetComponent<Renderer>().material.mainTexture = textureOR;
				heights[layer]++;
				lastObject[layer] = panelObj;
			}
			if (task[i] == 'i')
			{
				panelObj = Instantiate (panel, transform.position, transform.rotation) as GameObject;
				panelObj.name = "Panel";
				panelObj.transform.parent = board.transform;
				panelObj.transform.localPosition = coordinates[layer][heights[layer]];
				panelObj.transform.Rotate(new Vector3(0,180,0));
				panelObj.GetComponent<Renderer>().material.mainTexture = textureIMPL;
				heights[layer]++;
				lastObject[layer] = panelObj;
			}
			if (task[i] == 'e')
			{
				panelObj = Instantiate (panel, transform.position, transform.rotation) as GameObject;
				panelObj.name = "Panel";
				panelObj.transform.parent = board.transform;
				panelObj.transform.localPosition = coordinates[layer][heights[layer]];
				panelObj.transform.Rotate(new Vector3(0,180,0));
				panelObj.GetComponent<Renderer>().material.mainTexture = textureEQUIV;
				heights[layer]++;
				lastObject[layer] = panelObj;
			}
			if (task[i] == '1')
			{
				panelObj = Instantiate (panel, transform.position, transform.rotation) as GameObject;
				panelObj.name = "Panel";
				panelObj.transform.parent = board.transform;
				panelObj.transform.localPosition = coordinates[layer][heights[layer]];
				panelObj.transform.Rotate(new Vector3(0,180,0));
				panelObj.GetComponent<Renderer>().material.mainTexture = texture1;
				heights[layer]++;
				lastObject[layer] = panelObj;
			}
			if (task[i] == '0')
			{
				panelObj = Instantiate (panel, transform.position, transform.rotation) as GameObject;
				panelObj.name = "Panel";
				panelObj.transform.parent = board.transform;
				panelObj.transform.localPosition = coordinates[layer][heights[layer]];
				panelObj.transform.Rotate(new Vector3(0,180,0));
				panelObj.GetComponent<Renderer>().material.mainTexture = texture0;
				heights[layer]++;
				lastObject[layer] = panelObj;
			}
			if (task[i] == 'b')
			{
				panelObj = Instantiate (panel, transform.position, transform.rotation) as GameObject;
				panelObj.name = "Panel" + k;
				k++;
				panelObj.tag = "BoolPanel";
				panelObj.transform.parent = board.transform;
				panelObj.transform.localPosition = coordinates[layer][heights[layer]];
				panelObj.transform.Rotate(new Vector3(0,180,0));
				panelObj.GetComponent<Renderer>().material.mainTexture = textureBlank;
				heights[layer]++;
				lastObject[layer] = panelObj;
			}
			if (task[i] == 'o')
			{
				panelObj = Instantiate (panel, transform.position, transform.rotation) as GameObject;
				panelObj.name = "Panel" + k;
				k++;
				panelObj.tag = "OperPanel";
				panelObj.transform.parent = board.transform;
				panelObj.transform.localPosition = coordinates[layer][heights[layer]];
				panelObj.transform.Rotate(new Vector3(0,180,0));
				panelObj.GetComponent<Renderer>().material.mainTexture = textureBlank;
				heights[layer]++;
				lastObject[layer] = panelObj;
			}
			if (layer > 0 && panelObj != null)
			{
				float x1 = panelObj.transform.localPosition.x;
				float y1 = panelObj.transform.localPosition.y;
				float x2 = lastObject[layer-1].transform.localPosition.x;
				float y2 = lastObject[layer-1].transform.localPosition.y;
				
				linkObj = Instantiate (link, transform.position, transform.rotation) as GameObject;
				linkObj.name = "Link";
				linkObj.transform.parent = board.transform;
				linkObj.transform.localPosition = new Vector3((x1+x2)/2,(y1+y2)/2);
				
				float angle = GetAngleBetweenObjects(x1,y1,x2,y2);        
				linkObj.transform.Rotate(0,0,angle);
				
				float length = (float)Math.Sqrt(Math.Pow (x2-x1,2) + Math.Pow (y2-y1,2));
				linkObj.transform.localScale = new Vector3(length,linkObj.transform.localScale.y,linkObj.transform.localScale.z);

                links[j] = linkObj;
                j++;
			}
		}
	}
}