#pragma strict

var eng;

var CS_Screen : GameObject;
var ScreenItself1 : Material;
var ScreenItself2 : Material;
var StatDisplay1 : GameObject;
var StatDisplay2 : GameObject;
var LectureStands : GameObject;
var QuizRoomA : GameObject;
var QRA_mat1 : Material;
var QRA_mat2 : Material;
var QRA_pic : Texture2D;

function Start () {
		//РУБИЛЬНИК ДЛЯ ВЫБОРА МЕЖДУ РУССКИМ И АНГЛИЙСКИМ ЯЗЫКАМИ
		SetLanguage(0);
		//Application.ExternalCall("GetLanguage");		
}

function SetLanguage(ifEnglish : int) {
	if (ifEnglish==1) {
		
		//Controls
		GetComponent.<RPGParser>().LBL = "Experience";
		GetComponent.<UI>().LBL1 = "Controls";
		GetComponent.<UI>().LBL2 = "LMB - Interaction";
		GetComponent.<UI>().LBL3 = "RMB - Camera";
		GetComponent.<UI>().LBL4 = "W/A/S/D - Movements";
		GetComponent.<UI>().LBL5 = "Space - Jump";
		GetComponent.<UI>().LBL6 = "Shift - Run";
		GetComponent.<UI>().LBL7 = "I - Hide experience";
		GetComponent.<UI>().LBL8 = "Z - Hide controls";
		
		//Hub for course selection
		CS_Screen.renderer.material = ScreenItself1;
		CS_Screen.GetComponent.<CourseSelection>().NewScreen = ScreenItself2;
		CS_Screen.GetComponent.<CourseSelection>().LBL1 = "Loading...";
		CS_Screen.GetComponent.<CourseSelection>().LBL2 = "A / Left Arrow - Previous course";
		CS_Screen.GetComponent.<CourseSelection>().LBL3 = "D / Right Arrow - Next course";
		CS_Screen.GetComponent.<CourseSelection>().LBL4 = "Enter - Select";
		
		//Statistic monitors at the end of the corridors
		GetComponent.<StatisticParser>().LBL1 = "Course";
		GetComponent.<StatisticParser>().LBL2 = "Theme";
		StatDisplay1.transform.Find("TextLabel1").GetComponent(TextMesh).text = "Progress";
		StatDisplay1.transform.Find("TextLabel2").GetComponent(TextMesh).text = "Time spent";
		StatDisplay2.transform.Find("TextLabel1").GetComponent(TextMesh).text = "Progress";
		StatDisplay2.transform.Find("TextLabel2").GetComponent(TextMesh).text = "Tests completed";
		StatDisplay2.transform.Find("TextLabel3").GetComponent(TextMesh).text = "Correct answers";
		StatDisplay2.transform.Find("TextLabel4").GetComponent(TextMesh).text = "Paragraphs viewed";
		StatDisplay2.transform.Find("TextLabel5").GetComponent(TextMesh).text = "Time spent";
		
		//Lecture Hall		
		LectureStands.transform.Find("1/StandGroup/BackButton").GetComponent(TextMesh).text = "Back to the paragraphs list";
		LectureStands.transform.Find("1/PicturesGroup/Tip").GetComponent(TextMesh).text = "Click on an image to see it enlarged on the opposite wall";
		LectureStands.transform.Find("1/PicturesGroup/Zoom/ZoomClose").GetComponent(TextMesh).text = "Close";
		LectureStands.transform.Find("1/PicturesGroup/LayoutButton").GetComponent(TextMesh).text = "Change layout";
		LectureStands.transform.Find("2/StandGroup/BackButton").GetComponent(TextMesh).text = "Back to the paragraphs list";
		LectureStands.transform.Find("2/PicturesGroup/Tip").GetComponent(TextMesh).text = "Click on an image to see it enlarged on the opposite wall";
		LectureStands.transform.Find("2/PicturesGroup/Zoom/ZoomClose").GetComponent(TextMesh).text = "Close";
		LectureStands.transform.Find("2/PicturesGroup/LayoutButton").GetComponent(TextMesh).text = "Change layout";
		LectureStands.transform.Find("3/StandGroup/BackButton").GetComponent(TextMesh).text = "Back to the paragraphs list";
		LectureStands.transform.Find("3/PicturesGroup/Tip").GetComponent(TextMesh).text = "Click on an image to see it enlarged on the opposite wall";
		LectureStands.transform.Find("3/PicturesGroup/Zoom/ZoomClose").GetComponent(TextMesh).text = "Close";
		LectureStands.transform.Find("3/PicturesGroup/LayoutButton").GetComponent(TextMesh).text = "Change layout";
		LectureStands.transform.Find("4/StandGroup/BackButton").GetComponent(TextMesh).text = "Back to the paragraphs list";
		LectureStands.transform.Find("4/PicturesGroup/Tip").GetComponent(TextMesh).text = "Click on an image to see it enlarged on the opposite wall";
		LectureStands.transform.Find("4/PicturesGroup/Zoom/ZoomClose").GetComponent(TextMesh).text = "Close";
		LectureStands.transform.Find("4/PicturesGroup/LayoutButton").GetComponent(TextMesh).text = "Change layout";
		LectureStands.transform.Find("5/StandGroup/BackButton").GetComponent(TextMesh).text = "Back to the paragraphs list";
		LectureStands.transform.Find("5/PicturesGroup/Tip").GetComponent(TextMesh).text = "Click on an image to see it enlarged on the opposite wall";
		LectureStands.transform.Find("5/PicturesGroup/Zoom/ZoomClose").GetComponent(TextMesh).text = "Close";
		LectureStands.transform.Find("5/PicturesGroup/LayoutButton").GetComponent(TextMesh).text = "Change layout";
		LectureStands.transform.Find("6/StandGroup/BackButton").GetComponent(TextMesh).text = "Back to the paragraphs list";
		LectureStands.transform.Find("6/PicturesGroup/Tip").GetComponent(TextMesh).text = "Click on an image to see it enlarged on the opposite wall";
		LectureStands.transform.Find("6/PicturesGroup/Zoom/ZoomClose").GetComponent(TextMesh).text = "Close";
		LectureStands.transform.Find("6/PicturesGroup/LayoutButton").GetComponent(TextMesh).text = "Change layout";
		LectureStands.transform.Find("7/StandGroup/BackButton").GetComponent(TextMesh).text = "Back to the paragraphs list";
		LectureStands.transform.Find("7/PicturesGroup/Tip").GetComponent(TextMesh).text = "Click on an image to see it enlarged on the opposite wall";
		LectureStands.transform.Find("7/PicturesGroup/Zoom/ZoomClose").GetComponent(TextMesh).text = "Close";
		LectureStands.transform.Find("7/PicturesGroup/LayoutButton").GetComponent(TextMesh).text = "Change layout";
		
		//QuizRoomA
		QuizRoomA.transform.Find("Board Begin/Button").GetComponent(TextMesh).text = "Start the test";		
		QuizRoomA.transform.Find("Board Begin/Repeat").GetComponent(TextMesh).text = "Repeat";
		QuizRoomA.transform.Find("Board Static/FinishTestObject").GetComponent(TextMesh).text = "Finish the test";
		QuizRoomA.transform.Find("Board Static/FinishTestObject").transform.localPosition.x = 4.2;
		QuizRoomA.transform.Find("Board To Move/BoardGroup/Plane_LeftButton/Answers").renderer.material = QRA_mat1;
		QuizRoomA.transform.Find("Board To Move/BoardGroup/Plane_LeftButton/Question").renderer.material = QRA_mat2;
		QuizRoomA.transform.Find("Board To Move/BoardGroup/Plane_RightButton").renderer.material.mainTexture = QRA_pic;
		QuizRoomA.transform.Find("Board To Move/BoardGroup/Plane_RightButton").GetComponent.<Right_Button>().pic = QRA_pic;
		
		//плюс тексты с начислением очков см. в скриптах в папке РПГ
		//плюс см. StatisticParser, TeleportScript, TextLib и его соседей в скриптах лекции, FinishTest в тестовой
		
		eng = 1;
	}
}