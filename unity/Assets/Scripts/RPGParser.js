//#pragma strict

private var JSONTestString = "{"+
	"\"ifGuest\":false,\"username\":\"Student1\","+
	"\"EXP\":0,"+
	"\"facultyStands_Seen\":false,"+"\"facultyStands_Finish\":false,"+
	"\"historyStand_Seen\":false,"+"\"historyStand_Finish\":false,"+
	"\"scienceStand_Seen\":false,"+"\"scienceStand_Finish\":false,"+
	"\"staffStand_Seen\":false,"+"\"staffStand_Finish\":false,"+
	"\"logotypeJump\":false,"+"\"tableJump\":false,"+"\"terminalJump\":false,"+
	"\"ladderJump_First\":false,"+"\"ladderJump_All\":false,"+"\"letThereBeLight\":false,"+
	"\"plantJump_First\":false,"+"\"plantJump_Second\":false,"+"\"barrelRoll\":false,"+
	"\"firstVisitLecture\":false,"+"\"firstVisitTest\":false,"+
	"\"teleportations\":0,"+"\"paragraphsSeen\":1,"+"\"testsFinished\":0"+	
"}";

var RPG : OverallRPG;

var helvetica : Font;
private var displayHUD = false;
private var SkinSet = false;
private var UNwidthCalculated = false;
private var UNwidth : float;

private var displayAchievement = false;
private var displayAchievement_stage : int;
private var time_to_show : float;
private var pos_x = 0;
private var pos_y = 0;

private var achievementText : List.<String> = new List.<String>();
private var achievementPoints = new Array();
private var txt : String; private var pnt : int;
private var count = 0;
private var Player : GameObject;
var LBL = "Очки опыта";
private var nameOfAvatar;

function Start() {
	//БОЛЬШОЙ РУБИЛЬНИК
	//RoleSystemSet(JSONTestString);
    var httpConnector = new HttpConnector();
	httpConnector.GetGameAchievements();
}

function RoleSystemSet(JSONStringFromServer:String) {
	RPG = JsonFx.Json.JsonReader.Deserialize.<OverallRPG>(JSONStringFromServer);
	displayHUD = true;
}

function OnGUI() {
	if (!SkinSet) {
		//GUI.skin.box.font = helvetica; GUI.skin.box.fontSize = 12; GUI.skin.box.fontStyle = FontStyle.BoldAndItalic;
		//GUI.skin.label.font = helvetica; GUI.skin.label.fontStyle = FontStyle.Bold;
		//GUI.skin.label.fontSize = 12; GUI.skin.label.normal.textColor = Color(0.835, 0.929, 1);
		//GUI.skin.label.alignment = TextAnchor.MiddleCenter;
		//SkinSet = true;
	}
	if (displayHUD) {		
		if (RPG.ifGuest) GUI.Box(Rect(10, Screen.height - 50, 200, 40), LBL+": "+RPG.EXP.ToString());
		else {
			if (!UNwidthCalculated) {
				GUILayout.Box(RPG.username);
				UNwidth = GUILayoutUtility.GetLastRect().width;
				if (UNwidth > 1) UNwidthCalculated = true;
				if (UNwidth < 200) UNwidth = 200;
			}
			//GUI.Box(Rect(10, Screen.height - 95, UNwidth, 40), RPG.username);
			GUI.Box(Rect(10, Screen.height - 50, UNwidth, 40), LBL+": "+RPG.EXP.ToString());
		}		
	}
	if (displayAchievement) {
		if (displayAchievement_stage == 0) {
			txt = achievementText[0]; pnt = achievementPoints[0];
			displayAchievement_stage = 1;
		}
		if (displayAchievement_stage == 1) {
			GUI.skin.label.fontSize++;
			GUI.Label(Rect(0, Screen.height/4 - 25, Screen.width, 200), txt);			
			if (GUI.skin.label.fontSize >= 42) { displayAchievement_stage++; time_to_show = Time.timeSinceLevelLoad; }
		}
		else if (displayAchievement_stage == 2) {
			GUI.Label(Rect(0, Screen.height/4 - 25, Screen.width, 200), txt);
			if (Time.timeSinceLevelLoad - time_to_show > 3) displayAchievement_stage++;
		}
		else if (displayAchievement_stage == 3) {
			pos_x += Screen.width/50;
			pos_y += Screen.height*0.75/50;
			GUI.Label(Rect(0, Screen.height/4 - 25 + pos_y, Screen.width - pos_x, 200), txt);
			GUI.skin.label.fontSize--;
			if (GUI.skin.label.fontSize <= 0) {
				RPG.EXP += pnt; Save();
				GUI.skin.label.fontSize = 0; pos_x = 0; pos_y = 0;
				count--;
				if (count == 0)	displayAchievement = false;
				else {
					displayAchievement_stage = 0;
					for (var i=0; i<count; i++) {
						achievementText[i] = achievementText[i+1];
						achievementPoints[i] = achievementPoints[i+1];
					}
				}	
			}	
		}
	}	
}

function Update() {
	if (Input.GetKeyDown(KeyCode.I)) displayHUD = !displayHUD;
}

//@Deprecated
function Achievement(text:String, points:int) {	
	count++;
	achievementText.Add("");
	achievementText[count-1] = text;
	achievementPoints[count-1] = points;	
	if (!displayAchievement) {
		displayAchievement = true;
		displayAchievement_stage = 0;
	}
}

function ShowAchievement(achievementId : String) {	
}

//@Deprecated
function Save() {
	if (!RPG.ifGuest) {
		var s = JsonFx.Json.JsonWriter.Serialize(RPG);
		//Application.ExternalCall("SaveRPG", s);
	    var httpConnector = new HttpConnector();
    	httpConnector.SaveRPG(s); 
	}
}

function SaveAchievement(achievementId : String) {
	if (!RPG.ifGuest) {
	    var httpConnector = new HttpConnector();
		httpConnector.SaveGameAchievement(achievementId); 
	}
}