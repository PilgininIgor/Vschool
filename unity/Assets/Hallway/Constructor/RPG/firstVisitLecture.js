#pragma strict

var Bootstrap : GameObject;

function OnTriggerEnter() {
	var scr1 : RPGParser = Bootstrap.GetComponent.<RPGParser>();
	if (!scr1.RPG.firstVisitLecture) {
		scr1.Achievement("Первое посещение лекционного зала!\n+10 очков!", 10);
		scr1.RPG.firstVisitLecture = true;
	}
}