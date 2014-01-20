#pragma strict

class CoursesNamesList {
	var coursesNames : List.<CourseName>;
}

class CourseName {
	var id : String;
	var name : String;
}

private var JSONTestString = "{\"coursesNames\":["+
	"{\"id\":\"47942238-8168-404c-965e-4649b2a0278b\", \"name\":\"Информатика\"},"+
	"{\"id\":\"be549932-1844-4d2f-bded-f4b9aac61f97\", \"name\":\"Математика\"},"+
	"{\"id\":\"47942238-8168-404c-965e-111111111111\", \"name\":\"Физика\"}"+
	//"{\"id\":\"47942238-8168-404c-965e-222222222222\", \"name\":\"Больше физики\"}"+
"]}";

var LBL1 = "Загрузка...";
var LBL2 = "A / стрелка влево - предыдущий курс";
var LBL3 = "D / стрелка вправо - следующий курс";
var LBL4 = "Enter - загрузка";

var MainCam : Camera;
var StandCam : Camera;
var PlayerAvatar : GameObject;
var magnifier : Texture;
var arrow : Texture;
var helvetica : Font;

var NewPipe : Material;
var NewScreen : Material;
var dieSignals : SignalSender;

var cl : List.<CourseName>; var i : int;
private var hint_visible = false;
private var escape_visible = false;
private var data_loaded = false;

function Start() {
	StandCam.enabled = false; StandCam.GetComponent(AudioListener).enabled = false;	
}

function ZoomIn() {
	PlayerAvatar = GameObject.Find("Bootstrap").GetComponent.<NetworkManagerScript>().avatar;
	hint_visible = false;
	//PlayerAvatar.animation.Stop();
	//PlayerAvatar.SetActive(false);
	MainCam.enabled = false;
	MainCam.GetComponent(AudioListener).enabled = false;
	StandCam.enabled = true; StandCam.GetComponent(AudioListener).enabled = true;
	transform.parent.transform.Find("Menu").gameObject.SetActive(true);
	if (!data_loaded) {
		transform.parent.transform.Find("Menu/TextMain").GetComponent(TextMesh).text = LBL1;
		transform.parent.transform.Find("Menu/TextCounter").GetComponent(TextMesh).text = "";
		
		//БОЛЬШОЙ РУБИЛЬНИК
		CourseDisplay(JSONTestString);
		//Application.ExternalCall("LoadCoursesList");
		
	} else {
		escape_visible = true;
	}
}

function CourseDisplay(JSONStringFromServer : String) {
	var res : CoursesNamesList = JsonFx.Json.JsonReader.Deserialize.<CoursesNamesList>(JSONStringFromServer);						
	cl = res.coursesNames;
	i = 1;
	transform.parent.transform.Find("Menu/TextMain").GetComponent(TextMesh).text = cl[i-1].name;
	transform.parent.transform.Find("Menu/TextCounter").GetComponent(TextMesh).text = "1/"+cl.Count.ToString();	
	data_loaded = true; escape_visible = true;
}

function ZoomOut() {
	escape_visible = false;
	//PlayerAvatar.SetActive(true); PlayerAvatar.animation.Play("idle");
	MainCam.enabled = true; MainCam.GetComponent(AudioListener).enabled = true;
	StandCam.enabled = false; StandCam.GetComponent(AudioListener).enabled = false;
	transform.parent.transform.Find("Menu").gameObject.SetActive(false);	
}

function OnGUI() {
	if (hint_visible) if (GUI.Button(Rect(10,10,100,100), magnifier)) ZoomIn();
	if (escape_visible) {
		var old_font = GUI.skin.box.font;
		var old_fontSize = GUI.skin.box.fontSize;
		var old_fontStyle = GUI.skin.box.fontStyle;
		var old_textColor = GUI.skin.box.normal.textColor;
		var old_texture = GUI.skin.box.normal.background;
				
		GUI.skin.box.font = helvetica;
		GUI.skin.box.fontSize = 18;
		GUI.skin.box.fontStyle = FontStyle.Normal;
		GUI.skin.box.normal.textColor = Color.black;
		var texture = new Texture2D(1,1);
		texture.SetPixel(0,0, new Color(1,1,1,0.7));
		texture.Apply();
		GUI.skin.box.normal.background = texture;
		
		GUI.Box(Rect(Screen.width - 360, 10, 350, 37), LBL2);
		GUI.Box(Rect(Screen.width - 360, 60, 350, 37), LBL3);
		GUI.Box(Rect(Screen.width - 360, 110, 350, 37), LBL4);
		
		GUI.skin.box.font = old_font;
		GUI.skin.box.fontSize = old_fontSize;
		GUI.skin.box.fontStyle = old_fontStyle;
		GUI.skin.box.normal.textColor = old_textColor;
		GUI.skin.box.normal.background = old_texture;		
		
		if (GUI.Button(Rect(10,10,100,100), arrow)) ZoomOut();
	}	
}

function OnTriggerEnter(other: Collider) { hint_visible = true; }
function OnTriggerExit(other: Collider) { hint_visible = false; }

function Update() {
	if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && escape_visible) {
		i--; if (i <= 0) i = cl.Count;
		transform.parent.transform.Find("Menu/TextMain").GetComponent(TextMesh).text = cl[i-1].name;
		transform.parent.transform.Find("Menu/TextCounter").GetComponent(TextMesh).text = i.ToString()+"/"+cl.Count.ToString();
	}
	
	if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && escape_visible) {
		i++; if (i > cl.Count) i = 1;
		transform.parent.transform.Find("Menu/TextMain").GetComponent(TextMesh).text = cl[i-1].name;
		transform.parent.transform.Find("Menu/TextCounter").GetComponent(TextMesh).text = i.ToString()+"/"+cl.Count.ToString();
	}
	
	if 	(Input.GetKeyDown(KeyCode.Return) && escape_visible) {
		transform.parent.transform.Find("Menu/TextMain").GetComponent(TextMesh).text = LBL1;
		transform.parent.transform.Find("Menu/TextCounter").GetComponent(TextMesh).text = "";
		
		//БОЛЬШОЙ РУБИЛЬНИК
		/*var src1 : BootstrapParser = GameObject.Find("Bootstrap").GetComponent.<BootstrapParser>();
		src1.CourseConstructor(src1.JSONTestString);
		var src2 : StatisticParser = GameObject.Find("Bootstrap").GetComponent.<StatisticParser>();
		src2.StatisticDisplay(src2.JSONTestString);*/
		Application.ExternalCall("LoadCourseData", cl[i-1].id);
				
		dieSignals.SendSignals(this);
		this.renderer.material = NewScreen;
		transform.parent.transform.Find("UnlockPipe").renderer.material = NewPipe;
		escape_visible = false; data_loaded = false; //ZoomOut();
	}
}