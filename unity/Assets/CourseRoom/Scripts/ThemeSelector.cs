using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JsonFx.Json;

public class ThemeSelector : MonoBehaviour {

	public class ThemesNamesList
	{
		public List<ThemeName> themesNames;
	}

	[System.Serializable]
	public class ThemeName
	{
		public string id;
		public string name;
	}

	private string JSONTestString = "{\"coursesNames\":[" +
		"{\"id\":\"47942238-8168-404c-965e-4649b2a0278b\", \"name\":\"Информатика\"}," +
		"{\"id\":\"be549932-1844-4d2f-bded-f4b9aac61f97\", \"name\":\"Математика\"}," +
		"{\"id\":\"47942238-8168-404c-965e-111111111111\", \"name\":\"Физика\"}" +
		//"{\"id\":\"47942238-8168-404c-965e-222222222222\", \"name\":\"Больше физики\"}"+
		"]}";

	public const string LBL1 = "Загрузка...";
	public const string LBL2 = "A / стрелка влево - предыдущая тема";
	public const string LBL3 = "D / стрелка вправо - следующая тема";
	public const string LBL4 = "Enter - загрузка";
	public const string LBL5 = "Ошибка при загрузке списка тем...";

	public Camera GuiCam, MainCam, StandCam;
	public GameObject Player;
	public Texture magnifier, arrow, select, reload;
	public Font helvetica;

	public Material NewPipe, NewScreen;
	public SignalSender dieSignals;

	public List<ThemeName> cl;
	public int i;
	private bool hint_visible = false;
	private bool escape_visible = false;
	private bool data_loaded = false;
	private bool error_loading = false;

	private HttpConnector httpConnector;

	void Start()
	{
		StandCam.enabled = false; StandCam.GetComponent<AudioListener>().enabled = false;
		httpConnector = GameObject.Find("Bootstrap").GetComponent<HttpConnector>();
	}

	void ZoomIn()
	{
		Player = GameObject.Find("MainCamera").GetComponent<CameraFinC>().player;
		hint_visible = false;
		Player.SetActive(false);
		GuiCam.enabled = false;
		MainCam.enabled = false;
		MainCam.GetComponent<AudioListener>().enabled = false;
		StandCam.enabled = true; StandCam.GetComponent<AudioListener>().enabled = true;
		transform.parent.transform.Find("Menu").gameObject.SetActive(true);
		if (!data_loaded)
		{
			transform.parent.transform.Find("Menu/TextMain").GetComponent<TextMesh>().text = LBL1;
			transform.parent.transform.Find("Menu/TextCounter").GetComponent<TextMesh>().text = "";

			//БОЛЬШОЙ РУБИЛЬНИК
			LoadThemesList();
		}
		else
		{
			escape_visible = true;
		}
	}

	public void LoadThemesList()
	{
		var parameters = new Dictionary<string, string> ();
		parameters ["id"] = Global.courseId;
		if (httpConnector == null)
			httpConnector = GameObject.Find ("Bootstrap").GetComponent<HttpConnector> ();
		httpConnector.Post (HttpConnector.ServerUrl + HttpConnector.ThemesListUrl, parameters, www => {
			ThemeDisplay (www.text);
		},
			www => {
				transform.parent.transform.Find("Menu/TextMain").GetComponent<TextMesh>().text = LBL5;
				transform.parent.transform.Find("Menu/TextCounter").GetComponent<TextMesh>().text = "";
				data_loaded = false; escape_visible = true; error_loading = true;
			});
	}

	public void ThemeDisplay(string JSONStringFromServer)
	{
		ThemesNamesList res = JsonReader.Deserialize<ThemesNamesList>(JSONStringFromServer);
		cl = res.themesNames;
		i = 1;
		transform.parent.transform.Find("Menu/TextMain").GetComponent<TextMesh>().text = cl[i - 1].name;
		transform.parent.transform.Find("Menu/TextCounter").GetComponent<TextMesh>().text = "1/" + cl.Count.ToString();
		data_loaded = true; escape_visible = true; error_loading = false;
	}

	void ZoomOut()
	{
		escape_visible = false;
		Player.SetActive(true);
		GuiCam.enabled = true;
		MainCam.enabled = true; MainCam.GetComponent<AudioListener>().enabled = true;
		StandCam.enabled = false; StandCam.GetComponent<AudioListener>().enabled = false;
		transform.parent.transform.Find("Menu").gameObject.SetActive(false);
	}

	void OnGUI()
	{
		if (hint_visible)
		{
			if (GUI.Button(new Rect(DataStructures.buttonSize + 2 * DataStructures.buttonSpace, DataStructures.buttonSpace, DataStructures.buttonSize, DataStructures.buttonSize), magnifier)) ZoomIn();
		}
		if (escape_visible)
		{
			if (!error_loading) {
				if (GUI.Button (new Rect (DataStructures.buttonSize + 2 * DataStructures.buttonSpace, DataStructures.buttonSpace, DataStructures.buttonSize, DataStructures.buttonSize), arrow))
					ZoomOut ();
				if (GUI.Button (new Rect (Screen.width - DataStructures.buttonSize - DataStructures.buttonSpace, DataStructures.buttonSpace, DataStructures.buttonSize, DataStructures.buttonSize), select)) {
					transform.parent.transform.Find ("Menu/TextMain").GetComponent<TextMesh> ().text = LBL1;
					transform.parent.transform.Find ("Menu/TextCounter").GetComponent<TextMesh> ().text = "";

					//БОЛЬШОЙ РУБИЛЬНИК
					//Application.ExternalCall("LoadCourseData", cl[i-1].id);			
					LoadThemeData (cl [i - 1], i - 1);
				}
				var hUnit = Mathf.RoundToInt (Screen.height * DefaultSkin.LayoutScale);
				var wUnit = Mathf.RoundToInt (Screen.width * DefaultSkin.LayoutScale);
				var blockWidth = wUnit * 3 + 30;
				var blockHeight = hUnit * 3;
				var x = (Screen.width / 2) - (blockWidth / 2);
				var y = (Screen.height / 2) - (blockHeight / 2);
				wUnit /= 4;
				if (GUI.Button (new Rect (x, y, wUnit, hUnit), "<")) {
					i--;
					if (i <= 0)
						i = cl.Count;
					transform.parent.transform.Find ("Menu/TextMain").GetComponent<TextMesh> ().text = cl [i - 1].name;
					transform.parent.transform.Find ("Menu/TextCounter").GetComponent<TextMesh> ().text = i.ToString () + "/" + cl.Count.ToString ();
				}

				if (GUI.Button (new Rect (x + blockWidth, y, wUnit, hUnit), ">")) {
					i++;
					if (i > cl.Count)
						i = 1;
					transform.parent.transform.Find ("Menu/TextMain").GetComponent<TextMesh> ().text = cl [i - 1].name;
					transform.parent.transform.Find ("Menu/TextCounter").GetComponent<TextMesh> ().text = i.ToString () + "/" + cl.Count.ToString ();
				}
			} else {
				if (GUI.Button (new Rect (DataStructures.buttonSize + 2 * DataStructures.buttonSpace, DataStructures.buttonSpace, DataStructures.buttonSize, DataStructures.buttonSize), arrow))
					ZoomOut ();
				if (GUI.Button (new Rect (Screen.width - DataStructures.buttonSize - DataStructures.buttonSpace, DataStructures.buttonSpace, DataStructures.buttonSize, DataStructures.buttonSize), reload)) {
					LoadThemesList ();
				}
			}
		}
	}

	void OnTriggerEnter(Collider other) { hint_visible = true; }
	void OnTriggerExit(Collider other) { hint_visible = false; }

	public void LoadThemeData(ThemeName theme, int theme_num)
	{
		Global.themeId = theme.id;
		Global.themeName = theme.name;
		Global.theme_num = theme_num;

		GameObject.Find("TeleportBooth_ToTheme").GetComponent<TeleportToScene>().active = true;
		GameObject.Find("MonitorToTheme/Text").GetComponent<TitleMover>().ChangeTitle(Global.themeName);

		dieSignals.SendSignals(this);
		this.GetComponent<Renderer>().material = NewScreen;
		escape_visible = false; data_loaded = false; ZoomOut();
	}

	void Update()
	{
		if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && escape_visible)
		{
			i--; if (i <= 0) i = cl.Count;
			transform.parent.transform.Find("Menu/TextMain").GetComponent<TextMesh>().text = cl[i - 1].name;
			transform.parent.transform.Find("Menu/TextCounter").GetComponent<TextMesh>().text = i.ToString() + "/" + cl.Count.ToString();
		}

		if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && escape_visible)
		{
			i++; if (i > cl.Count) i = 1;
			transform.parent.transform.Find("Menu/TextMain").GetComponent<TextMesh>().text = cl[i - 1].name;
			transform.parent.transform.Find("Menu/TextCounter").GetComponent<TextMesh>().text = i.ToString() + "/" + cl.Count.ToString();
		}

		if (Input.GetKeyDown(KeyCode.Return) && escape_visible)
		{
			transform.parent.transform.Find("Menu/TextMain").GetComponent<TextMesh>().text = LBL1;
			transform.parent.transform.Find("Menu/TextCounter").GetComponent<TextMesh>().text = "";

			//БОЛЬШОЙ РУБИЛЬНИК
			//Application.ExternalCall("LoadCourseData", cl[i-1].id);
			LoadThemeData(cl[i - 1], i - 1);
		}
	}
}
