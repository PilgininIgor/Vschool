#pragma strict

var Bootstrap : GameObject;

function OnTriggerEnter() {
	var scr1 : RPGParser = Bootstrap.GetComponent.<RPGParser>();
	if (!scr1.RPG.barrelRoll) {
		scr1.Achievement("Сделана бочка!\n+10 очков!", 10);
		scr1.RPG.barrelRoll = true;
	}	
}