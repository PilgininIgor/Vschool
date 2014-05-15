using UnityEngine;
using System.Collections;

public class CharacterCust : MonoBehaviour
{
	//positions
	//float y = 2.0f, z = 31.0f, xLeft = 20.0f, xMiddle = 21.5f, xRigth = 23.0f;
	Vector3 leftPosition = new Vector3(20.0f, 2.0f, 31.0f);
	Vector3 middlePosition = new Vector3(21.5f, 2.0f, 31.0f);
	Vector3 rigthPosition = new Vector3(23.0f, 2.0f, 31.0f);
	
	//avatars
	public static string nameOfAvatar;
	
	public GameObject Robot;
	public GameObject Joan;
	public GameObject Alexis;
	public GameObject Golem;
    public GameObject Justin;
	public GameObject Vincent;
	public GameObject Solider;
	public GameObject Mia;
	
	//effects
	public GameObject Fire;
	public GameObject Sparks;
	public GameObject Explosions;
	public GameObject Fireworks;
	
	public GameObject Observation;
	
	int curCharacter = 0;
	int curEffect = 0;
	bool closerCamera = false;

	GameObject [] characters;
	GameObject [] effects;
	
	// Use this for initialization
	void Start () 
	{	
		characters = new GameObject[]{Joan, Alexis, Justin, Vincent, Solider, Mia, Robot};
		effects = new GameObject[]{Fire, Sparks, Explosions, Fireworks};
		ChangeCharecters(curCharacter);
		ChangeEffects(curEffect);
	}	
	
	void ZoomButton(int wRegularButton, int wBigButton, int hUnit)
	{
		GUILayout.BeginHorizontal();
		if (!closerCamera)
		{
            if (GUILayout.Button(Strings.Get("Look Around"), GUILayout.Width(wRegularButton), GUILayout.Height(hUnit)))
			{
				Observation.transform.Find("MainCamera").localPosition = new Vector3(2,3,-3);
				Observation.transform.Find("MainCamera").eulerAngles = new Vector3(0,325,0);
				closerCamera = true;
			}
		} else {
			Observation.transform.Find("MainCamera").RotateAround(
				Observation.transform.position - new Vector3(0,0,0.8f),
				Vector3.up,
				-20*Time.deltaTime
			);
            if (GUILayout.Button(Strings.Get("Stop Watch"), GUILayout.Width(wRegularButton), GUILayout.Height(hUnit)))
			{
				Observation.transform.Find("MainCamera").localPosition = new Vector3(2,3,-3);
				Observation.transform.Find("MainCamera").eulerAngles = new Vector3(0,325,0);
				closerCamera = false;
			}
		}
		GUILayout.EndHorizontal();
	}
	
	void commonButtons(int wRegularButton, int wBigButton, int hUnit)
	{
		GUILayout.BeginHorizontal();
		if (GUILayout.Button("<", GUILayout.Width(wRegularButton), GUILayout.Height(hUnit)) && curCharacter > 0)
		{
            ChangeCharecters(--curCharacter);
			if(curEffect == 0)
				curEffect = effects.Length - 1;
			else
				curEffect--;
			ChangeEffects(curEffect);
		}
		
        if(GUILayout.Button(Strings.Get("Go"), GUILayout.Width(wRegularButton), GUILayout.Height(hUnit)))
		{
			nameOfAvatar = characters[curCharacter].name;
			Debug.Log(nameOfAvatar);
            GameObject.Find("_Customization").AddComponent<PhotonMenu>();
			//Application.LoadLevel("world");
		}
        if (GUILayout.Button(">", GUILayout.Width(wRegularButton), GUILayout.Height(hUnit)) && (curCharacter < characters.Length - 1))
		{
           ChangeCharecters(++curCharacter);
			if(curEffect == effects.Length - 1)
				curEffect = 0;
			else
				curEffect++;
			ChangeEffects(curEffect);
		}
       GUILayout.EndHorizontal();
	}
		
	void OnGUI()
    {
		using (var skin = new DefaultSkin())
		{
			int hUnit = Mathf.RoundToInt(Screen.height * DefaultSkin.LayoutScale);
			int wUnit = Mathf.RoundToInt(Screen.width * DefaultSkin.LayoutScale);
			
			int wRegularButton = wUnit;
			int wBigButton = wUnit * 2;
			int wHugeButton = wUnit * 3;
			
			int blockWidth = wUnit * 3;
			int blockHeight = hUnit * 3;
			int x = (Screen.width/2) - (blockWidth / 2);
			int y = (Screen.height/2) - (blockHeight / 2);
			hUnit /= 2;
			
			GUILayout.BeginArea(new Rect(x, y , blockWidth + 15, blockHeight));
			
			commonButtons(wRegularButton, wBigButton, hUnit);
			
			GUILayout.EndArea();
			
			GUILayout.BeginArea(new Rect(x + wRegularButton + 5, y - hUnit - 5, blockWidth, blockHeight));
			
			ZoomButton(wRegularButton, wBigButton, hUnit);
			
			GUILayout.EndArea();
			
			
		}
	}
	
	
	void ChangeCharecters(int curCharacter)
	{	
		Debug.Log(characters[curCharacter].name + " # " + curCharacter);
		for(int i = 0; i < characters.Length; i++)
				characters[i].SetActive(false);
		characters[curCharacter].SetActive(true);
	}
	
	void ChangeEffects(int curEffect)
	{
		for(int i = 0; i < effects.Length; i++)
				effects[i].SetActive(false);
		effects[curEffect].SetActive(true);
	}
	

}