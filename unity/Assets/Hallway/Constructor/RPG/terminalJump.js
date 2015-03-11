#pragma strict

var Bootstrap : GameObject;

function OnTriggerEnter() {
	var scr1 : RPGParser = Bootstrap.GetComponent.<RPGParser>();
	if (!scr1.RPG.terminalJump) {
		scr1.Achievement("Прыжок на терминал!\n+10 очков!", 10);
		scr1.RPG.terminalJump = true;
	}
}