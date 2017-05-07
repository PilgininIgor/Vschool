using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskSelector : MonoBehaviour {

	public static readonly string SceneNameQuiz = "QuizRoom";
	public static readonly string SceneNameLecture = "LectureRoom";
	public static readonly string SceneNameTask1 = "task1Room";
	public static readonly string SceneNameTask2 = "task2Room";
	public static readonly string SceneNameTask3 = "task3Room";
	public static readonly string SceneNameIsland = "Islands";

	public class TasksNamesList
	{
		public List<TaskName> tasksNames;
	}

	[System.Serializable]
	public class TaskName
	{
		public string id;
		public string name;
		public string type;
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

	public Camera GuiCam, MainCam, StandCam;
	public GameObject Player;
	public Texture magnifier, arrow, select;
	public Font helvetica;

	public Material NewPipe, NewScreen;
	public SignalSender dieSignals;

	public List<TaskName> cl;
	public int i;
	private bool hint_visible = false;
	private bool escape_visible = false;
	private bool data_loaded = false;

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
			LoadTasksList();
		}
		else
		{
			escape_visible = true;
		}
	}

	public void LoadTasksList()
	{
		//переписать, чтобы грузилось с сервера
		TaskDisplay(Global.themeData);

	}

	public void TaskDisplay(DataStructures.Theme theme)
	{
		TasksNamesList res = new TasksNamesList ();
		res.tasksNames = new List<TaskName> ();
		for (int x = 0; x < theme.contents.Count; x++) {
			TaskName task = new TaskName ();
			task.id = theme.contents [x].id;
			task.name = theme.contents [x].name;
			task.type = theme.contents [x].type;
			res.tasksNames.Add (task);
		}

		cl = res.tasksNames;
		i = 1;
		transform.parent.transform.Find("Menu/TextMain").GetComponent<TextMesh>().text = cl[i - 1].name + " (" + cl[i - 1].type + ")";
		transform.parent.transform.Find("Menu/TextCounter").GetComponent<TextMesh>().text = "1/" + cl.Count.ToString();
		data_loaded = true; escape_visible = true;
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
			if (GUI.Button(new Rect(DataStructures.buttonSize + 2 * DataStructures.buttonSpace, DataStructures.buttonSpace, DataStructures.buttonSize, DataStructures.buttonSize), arrow)) ZoomOut();
			if (GUI.Button(new Rect(Screen.width - DataStructures.buttonSize - DataStructures.buttonSpace, DataStructures.buttonSpace, DataStructures.buttonSize, DataStructures.buttonSize), select))
			{
				transform.parent.transform.Find("Menu/TextMain").GetComponent<TextMesh>().text = LBL1;
				transform.parent.transform.Find("Menu/TextCounter").GetComponent<TextMesh>().text = "";

				//БОЛЬШОЙ РУБИЛЬНИК
				//Application.ExternalCall("LoadCourseData", cl[i-1].id);			
				LoadTaskData(cl[i - 1].id);
			}
			var hUnit = Mathf.RoundToInt(Screen.height * DefaultSkin.LayoutScale);
			var wUnit = Mathf.RoundToInt(Screen.width * DefaultSkin.LayoutScale);
			var blockWidth = wUnit * 3 + 30;
			var blockHeight = hUnit * 3;
			var x = (Screen.width / 2) - (blockWidth / 2);
			var y = (Screen.height / 2) - (blockHeight / 2);
			wUnit /= 4;
			if (GUI.Button(new Rect(x, y, wUnit, hUnit), "<"))
			{
				i--; if (i <= 0) i = cl.Count;
				transform.parent.transform.Find("Menu/TextMain").GetComponent<TextMesh>().text = cl[i - 1].name  + " (" + cl[i - 1].type + ")";
				transform.parent.transform.Find("Menu/TextCounter").GetComponent<TextMesh>().text = i.ToString() + "/" + cl.Count.ToString();
			}

			if (GUI.Button(new Rect(x + blockWidth, y, wUnit, hUnit), ">"))
			{
				i++; if (i > cl.Count) i = 1;
				transform.parent.transform.Find("Menu/TextMain").GetComponent<TextMesh>().text = cl[i - 1].name  + " (" + cl[i - 1].type + ")";
				transform.parent.transform.Find("Menu/TextCounter").GetComponent<TextMesh>().text = i.ToString() + "/" + cl.Count.ToString();
			}
		}
	}

	void OnTriggerEnter(Collider other) { hint_visible = true; }
	void OnTriggerExit(Collider other) { hint_visible = false; }

	public void LoadTaskData(string id)
	{
		var parameters = new Dictionary<string, string> ();
		parameters ["id"] = id;
		if (httpConnector == null)
			httpConnector = GameObject.Find ("Bootstrap").GetComponent<HttpConnector> ();
		
		int test_num = 0, lec_num = 0;
		for (int x = 0; x < Global.themeData.contents.Count; x++) {
			if (Global.themeData.contents [x].id == id) {
				switch (Global.themeData.contents [x].type) {
				case "test":
					Global.content_num = test_num;
					break;
				case "lecture":
					Global.content_num = lec_num;
					break;
				default:
					break;
				}
				Global.taskData = Global.themeData.contents [x];
				break;
			}
			switch (Global.themeData.contents [x].type) {
			case "test":
				test_num++; 
				break;
			case "lecture":
				lec_num++;
				break;
			default:
				break;
			}

		}

		string sceneName = "";
		switch (Global.taskData.type) {
		case "test":
			sceneName = SceneNameQuiz;
			break;
		case "lecture":
			sceneName = SceneNameLecture;
			break;
		case "task1":
			sceneName = SceneNameTask1;
			break;
		case "task2":
			sceneName = SceneNameTask2;
			break;
		case "task3":
			sceneName = SceneNameTask3;
			break;
		case "island":
			sceneName = SceneNameIsland;
			break;  

		default:
			break;
		}


		Global.taskId = id;
		GameObject.Find ("TeleportBooth_ToTask").GetComponent<TeleportToScene> ().active = true;
		Global.content = Global.taskData;
		GameObject.Find ("TeleportBooth_ToTask").GetComponent<TeleportToScene> ().SceneNameRoom = sceneName;
		GameObject.Find ("MonitorToTask/Text").GetComponent<TextMesh> ().text = Global.taskData.name + " (" + sceneName + ")";

		dieSignals.SendSignals (this);
		this.GetComponent<Renderer> ().material = NewScreen;
		escape_visible = false;
		data_loaded = false;
		ZoomOut ();
	}

	void Update()
	{
		if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && escape_visible)
		{
			i--; if (i <= 0) i = cl.Count;
			transform.parent.transform.Find("Menu/TextMain").GetComponent<TextMesh>().text = cl[i - 1].name  + " (" + cl[i - 1].type + ")";
			transform.parent.transform.Find("Menu/TextCounter").GetComponent<TextMesh>().text = i.ToString() + "/" + cl.Count.ToString();
		}

		if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && escape_visible)
		{
			i++; if (i > cl.Count) i = 1;
			transform.parent.transform.Find("Menu/TextMain").GetComponent<TextMesh>().text = cl[i - 1].name  + " (" + cl[i - 1].type + ")";
			transform.parent.transform.Find("Menu/TextCounter").GetComponent<TextMesh>().text = i.ToString() + "/" + cl.Count.ToString();
		}

		if (Input.GetKeyDown(KeyCode.Return) && escape_visible)
		{
			transform.parent.transform.Find("Menu/TextMain").GetComponent<TextMesh>().text = LBL1;
			transform.parent.transform.Find("Menu/TextCounter").GetComponent<TextMesh>().text = "";

			//БОЛЬШОЙ РУБИЛЬНИК
			//Application.ExternalCall("LoadCourseData", cl[i-1].id);
			LoadTaskData(cl[i - 1].id);
		}
	}
}
