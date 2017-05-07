using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ThemeStat : MonoBehaviour {

	private const string LBL2 = "Тема";

	public GameObject statDisplay;

	void Start () {
		statDisplay = GameObject.Find ("StatDisplay").gameObject;

		//Global.stat = JsonFx.Json.JsonReader.Deserialize<DataStructures.CourseRun>(Global.stats_json);

		statDisplay.transform.Find("TextTheme").GetComponent<TextMesh>().text = string.Format("{0} \"{1}\"", LBL2, Global.stat.themesRuns[Global.theme_num].name);
		statDisplay.transform.Find("TextTests").GetComponent<TextMesh>().text = string.Format("{0}/{1}", Global.stat.themesRuns[Global.theme_num].testsComplete, Global.stat.themesRuns[Global.theme_num].testsOverall);

		if ((!Global.stat.themesRuns[Global.theme_num].allTests) &&
			(Global.stat.themesRuns[Global.theme_num].testsComplete == Global.stat.themesRuns[Global.theme_num].testsOverall) &&
			(Global.stat.themesRuns[Global.theme_num].testsOverall != 0))
		{
			//            GetComponent<RPGParser>().Achievement("Пройдены все тесты в теме!\n+100 очков!", 100);
			//            stat.themesRuns[index - 1].allTests = true;
		}

		//суммирование верных ответов и общего количества ответов по всей теме
		int aac = 0, aao = 0;
		foreach (var t in Global.stat.themesRuns[Global.theme_num].testsRuns)
		{
			aac += t.answersCorrect;
			aao += t.answersOverall;
		}
		statDisplay.transform.Find("TextAnswers").GetComponent<TextMesh>().text = string.Format("{0}/{1}", aac, aao);
		if ((!Global.stat.themesRuns[Global.theme_num].allTestsMax) && (aac == aao) && (aao != 0))
		{
			//            GetComponent<RPGParser>().Achievement("Все тесты в теме пройдены идеально!\n+150 очков!", 150);
			//            Global.stat.themesRuns[index - 1].allTestsMax = true;
		}

		//суммирование просмотренных параграфов и их общего количества по всей теме
		int aps = 0, apo = 0;
		foreach (var lectureRun in Global.stat.themesRuns[Global.theme_num].lecturesRuns)
		{
			aps += lectureRun.paragraphsRuns.Count(x => x.haveSeen);
			apo += lectureRun.paragraphsRuns.Count;
		}
		statDisplay.transform.Find("TextParagraphs").GetComponent<TextMesh>().text =
			string.Format("{0}/{1}", aps, apo);

		if ((!Global.stat.themesRuns[Global.theme_num].allLectures) && (aps == apo) && (apo != 0))
		{
			//            GetComponent<RPGParser>().Achievement("Изучены все лекции по теме!\n+100 очков!", 100);
			//            Global.stat.themesRuns[index - 1].allLectures = true;
		}

		//обновление прогресса
		if ((aao != 0) && (apo != 0)) Global.stat.themesRuns[Global.theme_num].progress = (float)((aac + aps) * 100.0 / (aao + apo));
		else if (aao != 0) Global.stat.themesRuns[Global.theme_num].progress = (float)(aac * 100.0 / aao);
		else if (apo != 0) Global.stat.themesRuns[Global.theme_num].progress = (float)(aps * 100.0 / apo);
		statDisplay.transform.Find("TextProgress").GetComponent<TextMesh>().text =
			Mathf.RoundToInt(Global.stat.themesRuns[Global.theme_num].progress) + "%";

		if ((!Global.stat.themesRuns[Global.theme_num].completeAll) && (Mathf.RoundToInt(Global.stat.themesRuns[Global.theme_num].progress) == 100))
		{
			//            GetComponent<RPGParser>().Achievement("Тема пройдена на 100%!\n+250 очков!", 250);
			//            Global.stat.themesRuns[index - 1].completeAll = true;
		}

		//обновление прогресса всего курса
		Global.stat.progress = 0;
		foreach (var t in Global.stat.themesRuns)
			Global.stat.progress += t.progress;
		Global.stat.progress /= Global.stat.themesRuns.Count;
		/*bsParser.statDisplays[0].transform.Find("TextProgress").GetComponent<TextMesh>().text =
			Mathf.RoundToInt(stat.progress) + "%";
*/
		if ((!Global.stat.completeAll) && (Mathf.RoundToInt(Global.stat.progress) == 100))
		{
			//            GetComponent<RPGParser>().Achievement("Курс пройден на 100%!\n+1000 очков!", 1000);
			//            Global.stat.completeAll = true;
		}
	}
}
