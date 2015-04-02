using System.Linq;
using UnityEngine;

class StatisticParser : MonoBehaviour
{
    const string JSONTestString = "{" +
    "\"id\":\"00000000-0000-0000-000000000000\",\"mode\":\"registered\"," +
    "\"name\":\"Информатика\",\"progress\":0.0,\"timeSpent\":0.0," +
    "\"visited\":false,\"completeAll\":false," +
    "\"themesRuns\":[" +
        "{\"id\":\"11111111-1111-1111-1111-111111111111\",\"name\":\"Системы счисления\"," +
        "\"allLectures\":false,\"allTests\":false,\"allTestsMax\":false,\"completeAll\":false," +
        "\"progress\":0.0,\"testsComplete\":0,\"testsOverall\":1,\"timeSpent\":0.0,\"testsRuns\":[" +
            "{\"answersMinimum\":0,\"answersCorrect\":0,\"answersOverall\":0}" +
        "],\"lecturesRuns\":[" +
            "{\"timeSpent\":0.0,\"paragraphsRuns\":[]}" +
        "]}," +
        "{\"id\":\"22222222-2222-2222-2222-222222222222\",\"name\":\"Типы данных\"," +
        "\"allLectures\":false,\"allTests\":false,\"allTestsMax\":false,\"completeAll\":false," +
        "\"progress\":0.0,\"testsComplete\":0,\"testsOverall\":2,\"timeSpent\":0.0,\"testsRuns\":[" +
            "{\"answersMinimum\":0,\"answersCorrect\":0,\"answersOverall\":0}," +
            "{\"answersMinimum\":0,\"answersCorrect\":0,\"answersOverall\":0}" +
        "],\"lecturesRuns\":[" +
            "{\"timeSpent\":0.0,\"paragraphsRuns\":[]}," +
            "{\"timeSpent\":0.0,\"paragraphsRuns\":[]}" +
        "]}," +
        "{\"id\":\"33333333-3333-3333-3333-333333333333\",\"name\":\"Алгоритмы\"," +
        "\"allLectures\":false,\"allTests\":false,\"allTestsMax\":false,\"completeAll\":false," +
        "\"progress\":0.0,\"testsComplete\":0,\"testsOverall\":1,\"timeSpent\":0.0,\"testsRuns\":[" +
            "{\"answersMinimum\":0,\"answersCorrect\":0,\"answersOverall\":0}" +
        "],\"lecturesRuns\":[" +
            "{\"timeSpent\":0.0,\"paragraphsRuns\":[]}" +
        "]}," +
        "{\"id\":\"44444444-4444-4444-4444-444444444444\",\"name\":\"Языки программирования\"," +
        "\"allLectures\":false,\"allTests\":false,\"allTestsMax\":false,\"completeAll\":false," +
        "\"progress\":0.0,\"testsComplete\":0,\"testsOverall\":4,\"timeSpent\":0.0,\"testsRuns\":[" +
            "{\"answersMinimum\":0,\"answersCorrect\":0,\"answersOverall\":0}," +
            "{\"answersMinimum\":0,\"answersCorrect\":0,\"answersOverall\":0}," +
            "{\"answersMinimum\":0,\"answersCorrect\":0,\"answersOverall\":0}," +
            "{\"answersMinimum\":0,\"answersCorrect\":0,\"answersOverall\":0}" +
        "],\"lecturesRuns\":[" +
            "{\"timeSpent\":0.0,\"paragraphsRuns\":[]}," +
            "{\"timeSpent\":0.0,\"paragraphsRuns\":[]}," +
            "{\"timeSpent\":0.0,\"paragraphsRuns\":[]}" +
        "]}," +
        "{\"id\":\"55555555-5555-5555-5555-555555555555\",\"name\":\"Подготовка к ЕГЭ\"," +
        "\"allLectures\":false,\"allTests\":false,\"allTestsMax\":false,\"completeAll\":false," +
        "\"progress\":0.0,\"testsComplete\":0,\"testsOverall\":3,\"timeSpent\":0.0,\"testsRuns\":[" +
            "{\"answersMinimum\":3,\"answersCorrect\":0,\"answersOverall\":6}," +
            "{\"answersMinimum\":5,\"answersCorrect\":0,\"answersOverall\":9}," +
            "{\"answersMinimum\":8,\"answersCorrect\":0,\"answersOverall\":15}" +
        "],\"lecturesRuns\":[" +
            "{\"timeSpent\":0.0,\"paragraphsRuns\":[" +
                "{\"haveSeen\":false},{\"haveSeen\":false},{\"haveSeen\":false},{\"haveSeen\":false}," +
                "{\"haveSeen\":false},{\"haveSeen\":false},{\"haveSeen\":false},{\"haveSeen\":false}," +
                "{\"haveSeen\":false},{\"haveSeen\":false},{\"haveSeen\":false},{\"haveSeen\":false}," +
                "{\"haveSeen\":false},{\"haveSeen\":false},{\"haveSeen\":false},{\"haveSeen\":false}," +
                "{\"haveSeen\":false},{\"haveSeen\":false},{\"haveSeen\":false},{\"haveSeen\":false}" +
            "]}," +
            "{\"timeSpent\":0.0,\"paragraphsRuns\":[" +
                "{\"haveSeen\":false},{\"haveSeen\":false},{\"haveSeen\":false},{\"haveSeen\":false}," +
                "{\"haveSeen\":false}" +
            "]}" +
        "]}" +
    "]" +
"}";

    private const string LBL1 = "Курс";
    private const string LBL2 = "Тема";

    public DataStructures.CourseRun stat;
    private BootstrapParser bsParser;

    public void StatisticDisplay(string json)
    {
        stat = JsonFx.Json.JsonReader.Deserialize<DataStructures.CourseRun>(json);
        bsParser = GetComponent<BootstrapParser>();
        bsParser.statDisplays[0].transform.Find("TextCourse").GetComponent<TextMesh>().text = string.Format("{0} \"{1}\"", LBL1, stat.name);
        bsParser.statDisplays[0].transform.Find("TextProgress").GetComponent<TextMesh>().text = string.Format("{0}%", stat.progress);
        for (var i = 1; i < bsParser.sdlng; i++)
            UpdateThemeStat(i);
    }

    public void UpdateThemeStat(int index)
    {
        bsParser.statDisplays[index].transform.Find("TextTheme").GetComponent<TextMesh>().text = string.Format("{0} \"{1}\"", LBL2, stat.themesRuns[index - 1].name);
        bsParser.statDisplays[index].transform.Find("TextTests").GetComponent<TextMesh>().text = string.Format("{0}/{1}", stat.themesRuns[index - 1].testsComplete, stat.themesRuns[index - 1].testsOverall);

        if ((!stat.themesRuns[index - 1].allTests) &&
            (stat.themesRuns[index - 1].testsComplete == stat.themesRuns[index - 1].testsOverall) &&
            (stat.themesRuns[index - 1].testsOverall != 0))
        {
            GetComponent<RPGParser>().Achievement("Пройдены все тесты в теме!\n+100 очков!", 100);
            stat.themesRuns[index - 1].allTests = true;
        }

        //суммирование верных ответов и общего количества ответов по всей теме
        int aac = 0, aao = 0;
        foreach (var t in stat.themesRuns[index - 1].testsRuns)
        {
            aac += t.answersCorrect;
            aao += t.answersOverall;
        }
        bsParser.statDisplays[index].transform.Find("TextAnswers").GetComponent<TextMesh>().text = string.Format("{0}/{1}", aac, aao);
        if ((!stat.themesRuns[index - 1].allTestsMax) && (aac == aao) && (aao != 0))
        {
            GetComponent<RPGParser>().Achievement("Все тесты в теме пройдены идеально!\n+150 очков!", 150);
            stat.themesRuns[index - 1].allTestsMax = true;
        }

        //суммирование просмотренных параграфов и их общего количества по всей теме
        int aps = 0, apo = 0;
        foreach (var lectureRun in stat.themesRuns[index - 1].lecturesRuns)
        {
            aps += lectureRun.paragraphsRuns.Count(x => x.haveSeen);
            apo += lectureRun.paragraphsRuns.Count;
        }
        bsParser.statDisplays[index].transform.Find("TextParagraphs").GetComponent<TextMesh>().text =
        string.Format("{0}/{1}", aps, apo);

        if ((!stat.themesRuns[index - 1].allLectures) && (aps == apo) && (apo != 0))
        {
            GetComponent<RPGParser>().Achievement("Изучены все лекции по теме!\n+100 очков!", 100);
            stat.themesRuns[index - 1].allLectures = true;
        }

        //обновление прогресса
        if ((aao != 0) && (apo != 0)) stat.themesRuns[index - 1].progress = (float)((aac + aps) * 100.0 / (aao + apo));
        else if (aao != 0) stat.themesRuns[index - 1].progress = (float)(aac * 100.0 / aao);
        else if (apo != 0) stat.themesRuns[index - 1].progress = (float)(aps * 100.0 / apo);
        bsParser.statDisplays[index].transform.Find("TextProgress").GetComponent<TextMesh>().text =
            Mathf.RoundToInt(stat.themesRuns[index - 1].progress) + "%";

        if ((!stat.themesRuns[index - 1].completeAll) && (Mathf.RoundToInt(stat.themesRuns[index - 1].progress) == 100))
        {
            GetComponent<RPGParser>().Achievement("Тема пройдена на 100%!\n+250 очков!", 250);
            stat.themesRuns[index - 1].completeAll = true;
        }

        //обновление прогресса всего курса
        stat.progress = 0;
        foreach (var t in stat.themesRuns)
            stat.progress += t.progress;
        stat.progress /= stat.themesRuns.Count;
        bsParser.statDisplays[0].transform.Find("TextProgress").GetComponent<TextMesh>().text =
            Mathf.RoundToInt(stat.progress) + "%";

        if ((!stat.completeAll) && (Mathf.RoundToInt(stat.progress) == 100))
        {
            GetComponent<RPGParser>().Achievement("Курс пройден на 100%!\n+1000 очков!", 1000);
            stat.completeAll = true;
        }
    }

    public void Save()
    {
        if (stat.mode != "guest")
        {
            var s = JsonFx.Json.JsonWriter.Serialize(stat);
            var httpConnector = new HttpConnector();
            httpConnector.SaveStatistic(s);
        }
    }
}
