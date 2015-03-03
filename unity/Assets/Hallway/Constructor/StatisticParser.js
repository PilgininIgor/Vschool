#pragma strict

var JSONTestString = "{"+
	"\"id\":\"00000000-0000-0000-000000000000\",\"mode\":\"registered\","+
	"\"name\":\"Информатика\",\"progress\":0.0,\"timeSpent\":0.0,"+
	"\"visited\":false,\"completeAll\":false,"+
	"\"themesRuns\":["+
		"{\"id\":\"11111111-1111-1111-1111-111111111111\",\"name\":\"Системы счисления\","+
		"\"allLectures\":false,\"allTests\":false,\"allTestsMax\":false,\"completeAll\":false,"+
		"\"progress\":0.0,\"testsComplete\":0,\"testsOverall\":1,\"timeSpent\":0.0,\"testsRuns\":["+
			"{\"answersMinimum\":0,\"answersCorrect\":0,\"answersOverall\":0}"+
		"],\"lecturesRuns\":["+
			"{\"timeSpent\":0.0,\"paragraphsRuns\":[]}"+
		"]},"+
		"{\"id\":\"22222222-2222-2222-2222-222222222222\",\"name\":\"Типы данных\","+
		"\"allLectures\":false,\"allTests\":false,\"allTestsMax\":false,\"completeAll\":false,"+
		"\"progress\":0.0,\"testsComplete\":0,\"testsOverall\":2,\"timeSpent\":0.0,\"testsRuns\":["+
			"{\"answersMinimum\":0,\"answersCorrect\":0,\"answersOverall\":0},"+
			"{\"answersMinimum\":0,\"answersCorrect\":0,\"answersOverall\":0}"+
		"],\"lecturesRuns\":["+
			"{\"timeSpent\":0.0,\"paragraphsRuns\":[]},"+
			"{\"timeSpent\":0.0,\"paragraphsRuns\":[]}"+
		"]},"+
		"{\"id\":\"33333333-3333-3333-3333-333333333333\",\"name\":\"Алгоритмы\","+
		"\"allLectures\":false,\"allTests\":false,\"allTestsMax\":false,\"completeAll\":false,"+
		"\"progress\":0.0,\"testsComplete\":0,\"testsOverall\":1,\"timeSpent\":0.0,\"testsRuns\":["+
			"{\"answersMinimum\":0,\"answersCorrect\":0,\"answersOverall\":0}"+
		"],\"lecturesRuns\":["+
			"{\"timeSpent\":0.0,\"paragraphsRuns\":[]}"+
		"]},"+
		"{\"id\":\"44444444-4444-4444-4444-444444444444\",\"name\":\"Языки программирования\","+
		"\"allLectures\":false,\"allTests\":false,\"allTestsMax\":false,\"completeAll\":false,"+
		"\"progress\":0.0,\"testsComplete\":0,\"testsOverall\":4,\"timeSpent\":0.0,\"testsRuns\":["+
			"{\"answersMinimum\":0,\"answersCorrect\":0,\"answersOverall\":0},"+
			"{\"answersMinimum\":0,\"answersCorrect\":0,\"answersOverall\":0},"+
			"{\"answersMinimum\":0,\"answersCorrect\":0,\"answersOverall\":0},"+
			"{\"answersMinimum\":0,\"answersCorrect\":0,\"answersOverall\":0}"+
		"],\"lecturesRuns\":["+
			"{\"timeSpent\":0.0,\"paragraphsRuns\":[]},"+
			"{\"timeSpent\":0.0,\"paragraphsRuns\":[]},"+
			"{\"timeSpent\":0.0,\"paragraphsRuns\":[]}"+
		"]},"+
		"{\"id\":\"55555555-5555-5555-5555-555555555555\",\"name\":\"Подготовка к ЕГЭ\","+
		"\"allLectures\":false,\"allTests\":false,\"allTestsMax\":false,\"completeAll\":false,"+
		"\"progress\":0.0,\"testsComplete\":0,\"testsOverall\":3,\"timeSpent\":0.0,\"testsRuns\":["+
			"{\"answersMinimum\":3,\"answersCorrect\":0,\"answersOverall\":6},"+
			"{\"answersMinimum\":5,\"answersCorrect\":0,\"answersOverall\":9},"+
			"{\"answersMinimum\":8,\"answersCorrect\":0,\"answersOverall\":15}"+
		"],\"lecturesRuns\":["+
			"{\"timeSpent\":0.0,\"paragraphsRuns\":["+
				"{\"haveSeen\":false},{\"haveSeen\":false},{\"haveSeen\":false},{\"haveSeen\":false},"+
				"{\"haveSeen\":false},{\"haveSeen\":false},{\"haveSeen\":false},{\"haveSeen\":false},"+
				"{\"haveSeen\":false},{\"haveSeen\":false},{\"haveSeen\":false},{\"haveSeen\":false},"+
				"{\"haveSeen\":false},{\"haveSeen\":false},{\"haveSeen\":false},{\"haveSeen\":false},"+
				"{\"haveSeen\":false},{\"haveSeen\":false},{\"haveSeen\":false},{\"haveSeen\":false}"+
			"]},"+
			"{\"timeSpent\":0.0,\"paragraphsRuns\":["+
				"{\"haveSeen\":false},{\"haveSeen\":false},{\"haveSeen\":false},{\"haveSeen\":false},"+
				"{\"haveSeen\":false}"+
			"]}"+
		"]}"+
	"]"+
"}";

var LBL1 = "Курс";
var LBL2 = "Тема";

var STAT : CourseRun;
var scr : BootstrapParser;
var lng : Languages;

function StatisticDisplay (JSONStringFromServer : String) {
//	var reader = new JsonFx.Json.JsonReader();
//	STAT = reader.Read.<CourseRun>(JSONStringFromServer);
	STAT = JsonFx.Json.JsonReader.Deserialize.<CourseRun>(JSONStringFromServer);
	
	scr = GetComponent.<BootstrapParser>();
	scr.statDisplays[0].transform.Find("TextCourse").GetComponent(TextMesh).text = LBL1+" \""+STAT.name+"\"";
	scr.statDisplays[0].transform.Find("TextProgress").GetComponent(TextMesh).text = STAT.progress.ToString()+"%";
	for (var i=1; i<scr.sdlng; i++)
		UpdateThemeStat(i);
		
	lng = GetComponent.<Languages>();	
}

function UpdateThemeStat(i : int) {
	scr.statDisplays[i].transform.Find("TextTheme").GetComponent(TextMesh).text =
			LBL2+" \""+STAT.themesRuns[i-1].name+"\"";
	scr.statDisplays[i].transform.Find("TextTests").GetComponent(TextMesh).text =
		STAT.themesRuns[i-1].testsComplete.ToString()+"/"+STAT.themesRuns[i-1].testsOverall.ToString();
		
	if ((!STAT.themesRuns[i-1].allTests) &&
		(STAT.themesRuns[i-1].testsComplete == STAT.themesRuns[i-1].testsOverall) &&
		(STAT.themesRuns[i-1].testsOverall != 0)) {
		if (!lng.eng) GetComponent.<RPGParser>().Achievement("Пройдены все тесты в теме!\n+100 очков!", 100);
		else GetComponent.<RPGParser>().Achievement("All tests in this theme are completed!\n100 points!", 100);
		STAT.themesRuns[i-1].allTests = true;
	}
	
	//суммирование верных ответов и общего количества ответов по всей теме
	var aac = 0; var aao = 0;
	for (var j=0; j<STAT.themesRuns[i-1].testsRuns.Count; j++) {
		aac += STAT.themesRuns[i-1].testsRuns[j].answersCorrect;
		aao += STAT.themesRuns[i-1].testsRuns[j].answersOverall;
	}
	scr.statDisplays[i].transform.Find("TextAnswers").GetComponent(TextMesh).text =
		aac.ToString()+"/"+aao.ToString();
	
	if ((!STAT.themesRuns[i-1].allTestsMax) && (aac == aao) && (aao != 0)) {
		if (!lng.eng) GetComponent.<RPGParser>().Achievement("Все тесты в теме пройдены идеально!\n+150 очков!", 150);
		else GetComponent.<RPGParser>().Achievement("All tests in this theme are completed perfectly!\n150 points!", 150);
		STAT.themesRuns[i-1].allTestsMax = true;
	}
	
	//суммирование просмотренных параграфов и их общего количества по всей теме
	var aps = 0; var apo = 0;
	for (j=0; j<STAT.themesRuns[i-1].lecturesRuns.Count; j++) {
		for (var k=0; k<STAT.themesRuns[i-1].lecturesRuns[j].paragraphsRuns.Count; k++)
			if (STAT.themesRuns[i-1].lecturesRuns[j].paragraphsRuns[k].haveSeen) aps++;
		apo += STAT.themesRuns[i-1].lecturesRuns[j].paragraphsRuns.Count;
	}	
	scr.statDisplays[i].transform.Find("TextParagraphs").GetComponent(TextMesh).text =
		aps.ToString()+"/"+apo.ToString();
		
	if ((!STAT.themesRuns[i-1].allLectures) && (aps == apo) && (apo != 0)) {
		if (!lng.eng) GetComponent.<RPGParser>().Achievement("Изучены все лекции по теме!\n+100 очков!", 100);
		else GetComponent.<RPGParser>().Achievement("All lectures in this theme are studied!\n100 points!", 100);
		STAT.themesRuns[i-1].allLectures = true;
	}
	
	//обновление прогресса
	if ((aao != 0) && (apo != 0)) STAT.themesRuns[i-1].progress = (aac + aps) * 100.0 / (aao + apo);
	else if (aao != 0) STAT.themesRuns[i-1].progress = aac * 100.0 / aao;
	else if (apo != 0) STAT.themesRuns[i-1].progress = aps * 100.0 / apo;
	scr.statDisplays[i].transform.Find("TextProgress").GetComponent(TextMesh).text =
		Mathf.RoundToInt(STAT.themesRuns[i-1].progress).ToString()+"%";
		
	if ((!STAT.themesRuns[i-1].completeAll) && (Mathf.RoundToInt(STAT.themesRuns[i-1].progress) == 100)) {
		if (!lng.eng) GetComponent.<RPGParser>().Achievement("Тема пройдена на 100%!\n+250 очков!", 250);
		else GetComponent.<RPGParser>().Achievement("100% completed theme!\n250 points!", 250);
		STAT.themesRuns[i-1].completeAll = true;
	}
			
	//обновление прогресса всего курса
	STAT.progress = 0;
	for (j=0; j<STAT.themesRuns.Count; j++) STAT.progress += STAT.themesRuns[j].progress;
	STAT.progress /= STAT.themesRuns.Count;
	scr.statDisplays[0].transform.Find("TextProgress").GetComponent(TextMesh).text =
		Mathf.RoundToInt(STAT.progress).ToString()+"%";
		
	if ((!STAT.completeAll) && (Mathf.RoundToInt(STAT.progress) == 100)) {
		if (!lng.eng) GetComponent.<RPGParser>().Achievement("Курс пройден на 100%!\n+1000 очков!", 1000);
		else GetComponent.<RPGParser>().Achievement("100% completed course!\n1000 points!", 1000);
		STAT.completeAll = true;	
	}
}

function Save() {
	//сохранение статистики (пересылка данных на сервер)
	var s : String = JsonFx.Json.JsonWriter.Serialize(STAT); //print(s);
	if (STAT.mode != "guest") {
		//Application.ExternalCall("SaveStatistic", s);
		    var httpConnector = new HttpConnector();
    		httpConnector.SaveStatistic(s); 
	}
}