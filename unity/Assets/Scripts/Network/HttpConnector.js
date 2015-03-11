#pragma strict

//TODO move to config file
private var serverUrl : String = "http://localhost:25565/ils2";
//private var serverUrl : String = "http://virtual.itschool.ssau.ru/";

private var courseDataUrl : String = "/Render/UnityData";
private var statUrl : String = "/Render/UnityStat";
private var saveStatisticUrl : String = "/Render/UnitySave";
private var unityListUrl : String = "/Render/UnityList";
private var unitySaveRPG : String = "/Render/UnitySaveRPG";
private var saveGameAchievementUrl : String = "/Render/SaveGameAchievement";
private var getGameAchievementsUrl : String = "/Render/GetGameAchievements";

function LoadCourseData(id : String) {
	var form : WWWForm = new WWWForm();
	form.AddField("id", id);
	var www : WWW = new WWW(serverUrl + courseDataUrl, form);
	yield www;	
	var bootstrapParser : BootstrapParser = GameObject.Find("Bootstrap").GetComponent.<BootstrapParser>();
	bootstrapParser.CourseConstructor(www.text);
	
	www = new WWW(serverUrl + statUrl, form);
	yield www;
	var statisticParser : StatisticParser = GameObject.Find("Bootstrap").GetComponent.<StatisticParser>();
	statisticParser.StatisticDisplay(www.text);
	www.Dispose();
}

function SaveStatistic(s : String) {
	var form : WWWForm = new WWWForm();
	form.AddField("s", s);
	var www : WWW = new WWW(serverUrl + saveStatisticUrl, form);
	yield www;
	www.Dispose();
}

function LoadCoursesList() {
	var www : WWW = new WWW(serverUrl + unityListUrl);
	yield www;
	var src1 : CourseSelection = GameObject.Find("Hallway/Course Selection/CS_Screen").GetComponent.<CourseSelection>();	
	src1.CourseDisplay(www.text);
	www.Dispose();
}

function SaveRPG(s : String) {
	var form : WWWForm = new WWWForm();
	form.AddField("s", s);
	var www : WWW = new WWW(serverUrl + unitySaveRPG, form);
	yield www;
	www.Dispose();
}

function SaveGameAchievemnt(achievementId : String) {
	var form : WWWForm = new WWWForm();
	form.AddField("achievementId", achievementId);
	var www : WWW = new WWW(serverUrl + saveGameAchievementUrl, form);
	yield www;
	//TODO handle result
	www.Dispose();
}

function GetGameAchievements() {
    var www : WWW = new WWW(serverUrl + getGameAchievementsUrl);
	yield www;
	//TODO handle result
	www.Dispose();
}