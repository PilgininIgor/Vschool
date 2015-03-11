#pragma strict

var Bootstrap : GameObject;

function OnTriggerEnter() {
	var scr1 : StatisticParser = Bootstrap.GetComponent.<StatisticParser>();
	var scr2 : RPGParser = Bootstrap.GetComponent.<RPGParser>();
	if (!scr1.STAT.visited) {
		scr2.Achievement("Первое посещение курса!\n+10 очков!", 10);
		scr1.STAT.visited = true;
		scr1.Save();
	}
}