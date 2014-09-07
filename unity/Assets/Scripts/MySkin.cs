using UnityEngine;
using System.Collections;

enum GuiSizes
{
	GSSmall,
	GSNormal,
	GSHuge
}

public class MySkin : object
{
	static GuiSizes currentSize;
	
	static MySkin()
	{
		Debug.Log(string.Format("Skin started with {0} system", SystemInfo.operatingSystem));
		
		if (SystemInfo.operatingSystem.Contains("iOS"))
		{
			currentSize = GuiSizes.GSNormal;
		}
		else
			currentSize = GuiSizes.GSSmall;
		
		Debug.Log(string.Format("Decided to use {0}", currentSize));
	}
	
	//This method sets GUI skin respecting all platforms
	public static void OnGUI () 
	{
		var font = Resources.Load("HelveticaWorld-Regular") as Font;
		
		switch(currentSize)
		{
			case (GuiSizes.GSNormal):
			{
				GUI.skin.box.font = font; 
				GUI.skin.button.font = font;
				GUI.skin.box.fontSize = 20; 
				GUI.skin.button.fontSize = 20;
				GUI.skin.box.fontStyle = FontStyle.Bold; 
				GUI.skin.button.fontStyle = FontStyle.Bold;
				GUI.skin.box.normal.textColor = Color.white; 
				GUI.skin.button.normal.textColor = Color.white;
			} break;
			case(GuiSizes.GSSmall):
			{
				GUI.skin.box.font = font; 
				GUI.skin.button.font = font;
				GUI.skin.box.fontSize = 14; 
				GUI.skin.button.fontSize = 14;
				GUI.skin.box.fontStyle = FontStyle.Bold; 
				GUI.skin.button.fontStyle = FontStyle.Bold;
				GUI.skin.box.normal.textColor = Color.white; 
				GUI.skin.button.normal.textColor = Color.white;
			} break;
			default:
				break;
		}
	}
}
