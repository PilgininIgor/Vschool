#pragma strict

var Bootstrap : GameObject;

function OnTriggerEnter() {
	var scr1 : RPGParser = Bootstrap.GetComponent.<RPGParser>();
	if (!scr1.RPG.logotypeJump) {
		scr1.Achievement("Прыжок в логотип!\n+10 очков!", 10);
		scr1.RPG.logotypeJump = true;
	}
}