#pragma strict

var Bootstrap : GameObject;

function OnTriggerEnter() {
	var scr1 : RPGParser = Bootstrap.GetComponent.<RPGParser>();
	if (!scr1.RPG.firstVisitTest) {
		scr1.Achievement("Первое посещение комнаты тестирования!\n+10 очков!", 10);
		scr1.RPG.firstVisitTest = true;
	}
}