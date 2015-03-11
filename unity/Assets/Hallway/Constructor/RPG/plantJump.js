#pragma strict

var Bootstrap : GameObject;
var triggered = false;

function OnTriggerEnter() {
	var scr1 : RPGParser = Bootstrap.GetComponent.<RPGParser>();
	if (!triggered) {
		triggered = true;
		if (!scr1.RPG.plantJump_First) {
			scr1.Achievement("Прыжок на цветы!\n+10 очков!", 10);
			scr1.RPG.plantJump_First = true;
		}
		else if (!scr1.RPG.plantJump_Second) {
			scr1.Achievement("Еще прыжок на цветы!\n+10 очков!", 10);
			scr1.RPG.plantJump_Second = true;
		}		
	}
}