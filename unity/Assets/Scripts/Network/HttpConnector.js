#pragma strict

//TODO move to config file
//private var serverUrl : String = "http://localhost:25565/ils2";
private var serverUrl : String = "http://virtual.itschool.ssau.ru/";

private var courseDataUrl : String = "/Render/UnityData";
private var statUrl : String = "/Render/UnityStat";
private var saveStatisticUrl : String = "/Render/UnitySave";
private var unityListUrl : String = "/Render/UnityList";
private var unitySaveRPG : String = "/Render/UnitySaveRPG";

//function LoadDataFromServer(parameters : Dictionary.<String, String>, additionalURL : String) {
//	var form : WWWForm = new WWWForm();
//
//	for(var pKey in parameters.Keys) {
//		form.AddField(pKey, parameters[pKey]); 
//	}
//
//	www = new WWW(serverUrl + additionalURL, form);
//	yield www;
//}

function LoadCourseData(id : String) {
	var form : WWWForm = new WWWForm();
	form.AddField("id", id);
	var www : WWW = new WWW(serverUrl + courseDataUrl, form);
	yield www;	
	var src1 : BootstrapParser = GameObject.Find("Bootstrap").GetComponent.<BootstrapParser>();
	src1.CourseConstructor(www.text);
	
	www = new WWW(serverUrl + statUrl, form);
	yield www;
	var src2 : StatisticParser = GameObject.Find("Bootstrap").GetComponent.<StatisticParser>();
	src2.StatisticDisplay(src2.JSONTestString);
	www.Dispose();
}

function SaveStatistic(s : String) {
	var form : WWWForm = new WWWForm();
	form.AddField("s", s);
	var www : WWW = new WWW(serverUrl + saveStatisticUrl, form);
	yield www;
}

function LoadCoursesList() {
	var www : WWW = new WWW(serverUrl + unityListUrl);
	yield www;
	var src1 : CourseSelection = GameObject.Find("Hallway/Course Selection/CS_Screen").GetComponent.<CourseSelection>();	
	src1.CourseDisplay(www.text);
}

function SaveRPG(s : String) {
	var form : WWWForm = new WWWForm();
	form.AddField("s", s);
	var www : WWW = new WWW(serverUrl + unitySaveRPG, form);
	yield www;
}