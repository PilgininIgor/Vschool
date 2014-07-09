#pragma strict

//телепортация завязана на конкретных скриптах камеры и управления персонажем, которые шли в поставке с Лерпзом
//но, наверное, в итоге все равно мы их слегка отредактируем и сделаем подходящими любым аватарам
//да, и главную камеру, как видно, предполагается иметь внутри персонажа
//так все равно удобнее

//ссылка на будку-соседа (ее для всех будок задаст конструктор BootstrapParser.js)
var Destination : GameObject;
//ссылка на персонажа
var Player : GameObject;
//телепортация начинается по заходу персонажа в триггер - вот только и в будке-получателе есть такой же триггер,
//поэтому, чтобы не было каскадной активации, вводится переменная be_ready_to_receive, которую будка-отправитель
//устанавливает будке-получателю в true перед тем, как переместить персонажа
var be_ready_to_receive = false;

function OnTriggerEnter () {
	Player = GameObject.Find("MainCamera").GetComponent.<OrbitCam>().player;
	Debug.Log("Destination.transform.position:" + Destination.transform.position);
	if (!be_ready_to_receive) {
		//если мы - отправитель, значит, начинаем
		Destination.GetComponent.<TeleportScript>().be_ready_to_receive = true;
		
		//включаем эффект, цилиндр начинает из прозрачного становиться белым
		transform.Find("TeleportEffect").animation.Play("TeleportEffect");
		//полностью белым он станет через 30 кадров - ждем их (yield ждет один кадр, отсюда и цикл)
		for (var i=0; i<30; i++) yield;
		//отключаем скрипт управления и скрываем геометрию персонажа
		//Player.GetComponent.<ThirdPersonControllerLerpz>().enabled = false;
 	 	//Player.renderer = false;
		//Player.transform.Find("Hips").gameObject.SetActive(false);
		//теперь цилиндр снова будет становиться прозрачным, но персонаж уже будет невидимым
		for (i=0; i<30; i++) yield;
		
		SaveStat();
		SetAchievement();
						
		//собственно телепортация
		//перемещаем персонажа в будку-получателя
		Debug.Log("Teleport Avatar:" + Player.name);
		Debug.Log("Player.transform.position:" + Player.transform.position);
		Debug.Log("Destination.transform.position:" + Destination.transform.position);
		Player.transform.position = Destination.transform.position;
		Debug.Log("Teleporting...");

		Debug.Log("Player.transform.position:" + Player.transform.position);
		//if (Player.name == "Worker") Player.transform.position.y += 1; //а то рабочий чет проваливается вниз
		//запоминаем угол, на который повернута будка-получатель
		var rt = Mathf.Round(Destination.transform.localRotation.eulerAngles.y);
		//ручками поворачиваем камеру так, чтобы она смотрела на будку
		GameObject.Find("MainCamera").GetComponent.<OrbitCam>().x = rt + 180;
		//поворачиваем персонажа так, чтобы он стоял лицом к выходу из будки
		
		Player.transform.rotation = Quaternion.AngleAxis(rt, Player.transform.up);
		
		//включаем эффект, теперь у получателя								
		Destination.transform.Find("TeleportEffect").animation.Play("TeleportEffect");
		for (i=0; i<30; i++) yield;
		//после того, как цилиндр достиг белого максимума, возвращаем персонажу видимость и управление
		//Player.transform.Find("rootJoint").gameObject.SetActive(true);
		//Player.renderer = true;
		/*Player.GetComponent.<ThirdPersonControllerLerpz>().enabled = true;*/
		
	} else {
		//если мы получатель, значит, просто сбрасываем переменную
		//когда игрок вернется, отправителем будем уже мы
		be_ready_to_receive = false;
	}
}

function SaveStat() {
	GameObject.Find("Bootstrap").GetComponent.<StatisticParser>().Save();	
}

function SetAchievement() {
	var scr : RPGParser = GameObject.Find("Bootstrap").GetComponent.<RPGParser>();	
	scr.RPG.teleportations += 1;
	if (!(GameObject.Find("Bootstrap").GetComponent.<Languages>().eng)) {
		if (scr.RPG.teleportations == 1) scr.Achievement("Первая телепортация!\n+10 очков!", 10);
		else if (scr.RPG.teleportations == 20) scr.Achievement("20 телепортаций!\n+30 очков!", 30);
		else if (scr.RPG.teleportations == 50) scr.Achievement("50 телепортаций!\n+50 очков!", 50);
		else if (scr.RPG.teleportations == 100) scr.Achievement("100 телепортаций!\n+80 очков!", 80);
		else scr.Save();
	} else {
		if (scr.RPG.teleportations == 1) scr.Achievement("First teleportation!\n10 points!", 10);
		else if (scr.RPG.teleportations == 20) scr.Achievement("20 teleportations!\n30 points!", 30);
		else if (scr.RPG.teleportations == 50) scr.Achievement("50 teleportations!\n50 points!", 50);
		else if (scr.RPG.teleportations == 100) scr.Achievement("100 teleportations!\n80 points!", 80);
		else scr.Save();
	}
}