using UnityEngine;
using System.Collections;

public class InsertItem : MonoBehaviour {
	
	string[] panels = new string[4] {"","","",""};
	public Texture textureAND;
	public Texture textureOR;
	public Texture textureIMPL;
	public Texture textureEQUIV;
	public Texture texture1;
	public Texture texture0;
	public Texture textureBlank;
	
	public void Insert (GameObject panelObject) 
	{
		string itemType = GameObject.Find ("hand").GetComponent<InstItem> ().CurrentItemType ();
		GameObject.Find ("hand").GetComponent<InstItem> ().RemoveCurrentItem ();
		
		if (itemType == "")
		{
			panelObject.GetComponent<Renderer>().material.mainTexture = textureBlank;
		}
		if (itemType == "AND")
		{
			panelObject.GetComponent<Renderer>().material.mainTexture = textureAND;
		}
		if (itemType == "OR")
		{
			panelObject.GetComponent<Renderer>().material.mainTexture = textureOR;
		}
		if (itemType == "IMPL")
		{
			panelObject.GetComponent<Renderer>().material.mainTexture = textureIMPL;
		}
		if (itemType == "EQUIV")
		{
			panelObject.GetComponent<Renderer>().material.mainTexture = textureEQUIV;
		}
		if (itemType == "0")
		{
			panelObject.GetComponent<Renderer>().material.mainTexture = texture0;
		}
		if (itemType == "1")
		{
			panelObject.GetComponent<Renderer>().material.mainTexture = texture1;
		}
		
		int panelNumber = int.Parse (panelObject.name.Substring (5, 1));
		
		if (panels[panelNumber] != "")
		{
			if (panels[panelNumber] == "AND")
			{
				GameObject.Find ("hand").GetComponent<InstItem> ().GenerateItem ("AND");
			}
			if (panels[panelNumber] == "OR")
			{
				GameObject.Find ("hand").GetComponent<InstItem> ().GenerateItem ("OR");
			}
			if (panels[panelNumber] == "IMPL")
			{
				GameObject.Find ("hand").GetComponent<InstItem> ().GenerateItem ("IMPL");
			}
			if (panels[panelNumber] == "EQUIV")
			{
				GameObject.Find ("hand").GetComponent<InstItem> ().GenerateItem ("EQUIV");
			}
			if (panels[panelNumber] == "0")
			{
				GameObject.Find ("hand").GetComponent<InstItem> ().GenerateItem ("0");
			}
			if (panels[panelNumber] == "1")
			{
				GameObject.Find ("hand").GetComponent<InstItem> ().GenerateItem ("1");
			}
		}
		panels [panelNumber] = itemType;
	}
	
	public string[] GetAnswer ()
	{
		return panels;
	}
}