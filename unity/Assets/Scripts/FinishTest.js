#pragma strict

var BoardStatic : GameObject; var BoardToMove : GameObject;
var Cube1 : GameObject; var Cube2 : GameObject; var Cube3 : GameObject; 
var Cube4 : GameObject; var Cube5 : GameObject; 
private var scr : Board;

function OnMouseDown() {
	BoardStatic.transform.localPosition.z = 18.5;
	BoardToMove.transform.Find("BoardGroup").animation.Play("BoardDisappear");
	
	//задвинуть кубы, а потом сделать их все неактивными
	scr = BoardToMove.transform.Find("BoardGroup").GetComponent.<Board>();
	if (scr.a[scr.i][0] == 0) Cube1.animation.Play("CubeInactiveDown"); else Cube1.animation.Play("CubeActiveDown");
	if (scr.a[scr.i][1] == 0) Cube2.animation.Play("CubeInactiveDown"); else Cube2.animation.Play("CubeActiveDown");
	if (scr.a[scr.i][2] == 0) Cube3.animation.Play("CubeInactiveDown"); else Cube3.animation.Play("CubeActiveDown");
	if (scr.qAnsNum[scr.i] > 3) {
		if (scr.a[scr.i][3] == 0) Cube4.animation.Play("CubeInactiveDown"); else Cube4.animation.Play("CubeActiveDown");
	}
	if (scr.qAnsNum[scr.i] > 4) {
		if (scr.a[scr.i][4] == 0) Cube5.animation.Play("CubeInactiveDown"); else Cube5.animation.Play("CubeActiveDown");
	}
	Cube1.GetComponent.<Cube>().is_active = false; Cube2.GetComponent.<Cube>().is_active = false;
	Cube3.GetComponent.<Cube>().is_active = false; Cube4.GetComponent.<Cube>().is_active = false;
	Cube5.GetComponent.<Cube>().is_active = false;
	
	//расчет времени ответа на последний (текущий активный) вопрос
	scr.t[scr.i] += Time.timeSinceLevelLoad - scr.prev_time;
	scr.prev_time = Time.timeSinceLevelLoad;
		
	//у нас много тестовых комнат, поэтому нужно обеспечить объекту уникальное имя
	name = "FinishTestObject_"+scr.test_id;
	//БОЛЬШОЙ РУБИЛЬНИК
	DisplayResults(scr.qText.length-2);
	//var sp = GameObject.Find("Bootstrap").GetComponent("StatisticParser");
	//Application.ExternalCall("SaveTestResult",
	//						 sp.STAT.mode, sp.STAT.themesRuns[scr.theme_num].id,
	//						 scr.test_id, scr.a, scr.t);
}

function DisplayResults(score : int) {
	var bs = GameObject.Find("Bootstrap");
	var sp : StatisticParser = bs.GetComponent.<StatisticParser>();
	var rpg : RPGParser = bs.GetComponent.<RPGParser>();
	var lng : Languages = bs.GetComponent.<Languages>();
	var tr = sp.STAT.themesRuns[scr.theme_num].testsRuns[scr.test_num];
	
	if (!lng.eng) {
		transform.parent.transform.parent.transform.Find("Board Begin/Result").GetComponent(TextMesh).text =
			"Ваш результат: "+score.ToString()+" из "+tr.answersOverall.ToString();		
		transform.parent.transform.parent.transform.Find("Board Begin/Minimum").GetComponent(TextMesh).text =
			"Требуемый минимум: "+tr.answersMinimum.ToString()+" из "+tr.answersOverall.ToString();	
	} else {
		transform.parent.transform.parent.transform.Find("Board Begin/Result").GetComponent(TextMesh).text =
			"Your result: "+score.ToString()+" of "+tr.answersOverall.ToString();
		transform.parent.transform.parent.transform.Find("Board Begin/Minimum").GetComponent(TextMesh).text =
			"Required minimum: "+tr.answersMinimum.ToString()+" of "+tr.answersOverall.ToString();
	}

	transform.parent.transform.parent.transform.Find("Board Begin/Minimum").localPosition.z = 0;
	transform.parent.transform.parent.transform.Find("Board Begin/Result").localPosition.z = 0;
	transform.parent.transform.parent.transform.Find("Board Begin/Repeat").localPosition.z = 0;
	transform.parent.transform.parent.transform.Find("Board Begin/Conclusion").localPosition.z = 0;
	
	if (score >= tr.answersMinimum) {
		if (!lng.eng) transform.parent.transform.parent.transform.Find("Board Begin/Conclusion").GetComponent(TextMesh).text = "Тест успешно пройден!";
		else transform.parent.transform.parent.transform.Find("Board Begin/Conclusion").GetComponent(TextMesh).text = "Test is passed successfully!";
		//если до сих пор в результатах число верных ответов было меньше минимума, значит,
		//юзер успешно прошел этот тест впервые и надо прибавить 1 к числу пройденных тестов,
		//а также заняться ачивментами
		if (tr.answersCorrect < tr.answersMinimum) {
			if (!lng.eng) {
				sp.STAT.themesRuns[scr.theme_num].testsComplete += 1;
				rpg.Achievement("Тест успешно пройден!\n+30 очков!", 30);
				rpg.RPG.testsFinished += 1;
				if (rpg.RPG.testsFinished == 1) rpg.Achievement("Первый пройденный тест!\n+20 очков!", 10);
				else if (rpg.RPG.testsFinished == 10) rpg.Achievement("Пройдено 10 тестов!\n+100 очков!", 100);
				else if (rpg.RPG.testsFinished == 25) rpg.Achievement("Пройдено 25 тестов!\n+200 очков!", 200);
				else if (rpg.RPG.testsFinished == 50) rpg.Achievement("Пройдено 50 тестов!\n+500 очков!", 500);
				else rpg.Save();
			} else {
				sp.STAT.themesRuns[scr.theme_num].testsComplete += 1;
				rpg.Achievement("Test is passed successfully!\n30 points!", 30);
				rpg.RPG.testsFinished += 1;
				if (rpg.RPG.testsFinished == 1) rpg.Achievement("First finished test!\n20 points!", 10);
				else if (rpg.RPG.testsFinished == 10) rpg.Achievement("10 finished tests!\n100 points!", 100);
				else if (rpg.RPG.testsFinished == 25) rpg.Achievement("25 finished tests!\n200 points!", 200);
				else if (rpg.RPG.testsFinished == 50) rpg.Achievement("50 finished tests!\n500 points!", 500);
				else rpg.Save();
			}
		}
	} else {
		if (!lng.eng) transform.parent.transform.parent.transform.Find("Board Begin/Conclusion").GetComponent(TextMesh).text = "Недостаточно верных ответов";
		else transform.parent.transform.parent.transform.Find("Board Begin/Conclusion").GetComponent(TextMesh).text = "Not enough correct answers";
	}
	if ((tr.answersCorrect < score) && (score == tr.answersOverall)) {
		if (!lng.eng) rpg.Achievement("Идеальный результат!\n+30 очков!", 30);
		else rpg.Achievement("Perfect score!\n30 points!", 30);
	}
	//и независимо от того, засчитан тест или нет, если это пока что лучший результат юзера, его надо запомнить
	if (tr.answersCorrect < score) tr.answersCorrect = score;
	sp.UpdateThemeStat(scr.theme_num+1);
}