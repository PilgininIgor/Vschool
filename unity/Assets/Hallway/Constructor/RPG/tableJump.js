#pragma strict

var Bootstrap : GameObject;

function OnTriggerEnter() {
	var scr1 : RPGParser = Bootstrap.GetComponent.<RPGParser>();
	if (!scr1.RPG.tableJump) {
		scr1.Achievement("Прыжок на стол!\n+10 очков!", 10);
		scr1.RPG.tableJump = true;
	}
}