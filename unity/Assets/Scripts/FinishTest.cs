using UnityEngine;

public class FinishTest : MonoBehaviour
{
    public GameObject BoardStatic, BoardToMove, Cube1, Cube2, Cube3, Cube4, Cube5;
    private Board board;
    void OnMouseDown()
    {
        var p = BoardStatic.transform.localPosition;
        p.z = 18.5f;
        BoardStatic.transform.localPosition = p;
        BoardToMove.transform.Find("BoardGroup").GetComponent<Animation>().Play("BoardDisappear");

        //задвинуть кубы, а потом сделать их все неактивными
        board = BoardToMove.transform.Find("BoardGroup").GetComponent<Board>();
        Cube1.GetComponent<Animation>().Play(!board.a[board.i][0] ? "CubeInactiveDown" : "CubeActiveDown");
        Cube2.GetComponent<Animation>().Play(!board.a[board.i][1] ? "CubeInactiveDown" : "CubeActiveDown");
        Cube3.GetComponent<Animation>().Play(!board.a[board.i][2] ? "CubeInactiveDown" : "CubeActiveDown");
        if (board.qAnsNum[board.i] > 3)
        {
            Cube4.GetComponent<Animation>().Play(!board.a[board.i][3] ? "CubeInactiveDown" : "CubeActiveDown");
        }
        if (board.qAnsNum[board.i] > 4)
        {
            Cube5.GetComponent<Animation>().Play(!board.a[board.i][4] ? "CubeInactiveDown" : "CubeActiveDown");
        }
        Cube1.GetComponent<Cube>().is_active = false; Cube2.GetComponent<Cube>().is_active = false;
        Cube3.GetComponent<Cube>().is_active = false; Cube4.GetComponent<Cube>().is_active = false;
        Cube5.GetComponent<Cube>().is_active = false;

        //расчет времени ответа на последний (текущий активный) вопрос
        board.t[board.i] += Time.timeSinceLevelLoad - board.prev_time;
        board.prev_time = Time.timeSinceLevelLoad;

        //у нас много тестовых комнат, поэтому нужно обеспечить объекту уникальное имя
        name = "FinishTestObject_" + board.test_id;
        //БОЛЬШОЙ РУБИЛЬНИК
		int score = CheckAnswers();
        DisplayResults(score);
        //var sp = GameObject.Find("Bootstrap").GetComponent("StatisticParser");
        //Application.ExternalCall("SaveTestResult",
        //						 sp.STAT.mode, sp.STAT.themesRuns[board.theme_num].id,
        //						 board.test_id, board.a, board.t);
    }

	int CheckAnswers() {
		int trueNum = 0;

		for (int i = 0; i < board.qAns.Length; i++) {
			bool isCorrect = true;
			for (int j = 0; j < board.qAnsNum [i]; j++) {
				if (board.a [i] [j] != board.trueAns [i] [j]) {
					isCorrect = false;
					break;
				}
			}
			if (isCorrect)
				trueNum++;
		}

		return trueNum;
	}

    void DisplayResults(int score)
    {
        var bs = GameObject.Find("Bootstrap");
        var rpg = bs.GetComponent<RPGParser>();
        var tr = Global.stat.themesRuns[board.theme_num].testsRuns[board.test_num];

        transform.parent.transform.parent.transform.Find("Board Begin/Result").GetComponent<TextMesh>().text =
            "Ваш результат: " + score.ToString() + " из " + tr.answersOverall.ToString();
        transform.parent.transform.parent.transform.Find("Board Begin/Minimum").GetComponent<TextMesh>().text =
            "Требуемый минимум: " + tr.answersMinimum.ToString() + " из " + tr.answersOverall.ToString();
        var p = transform.parent.transform.parent.transform.Find("Board Begin/Minimum").localPosition;
        p.z = 0;
        transform.parent.transform.parent.transform.Find("Board Begin/Minimum").localPosition = p;
        p = transform.parent.transform.parent.transform.Find("Board Begin/Result").localPosition;
        p.z = 0;
        transform.parent.transform.parent.transform.Find("Board Begin/Result").localPosition = p;
        p = transform.parent.transform.parent.transform.Find("Board Begin/Repeat").localPosition;
        p.z = 0;
        transform.parent.transform.parent.transform.Find("Board Begin/Repeat").localPosition = p;
        p = transform.parent.transform.parent.transform.Find("Board Begin/Conclusion").localPosition;
        p.z = 0;
        transform.parent.transform.parent.transform.Find("Board Begin/Conclusion").localPosition = p;

        if (score >= tr.answersMinimum)
        {
            transform.parent.transform.parent.transform.Find("Board Begin/Conclusion").GetComponent<TextMesh>().text = "Тест успешно пройден!";
            //если до сих пор в результатах число верных ответов было меньше минимума, значит,
            //юзер успешно прошел этот тест впервые и надо прибавить 1 к числу пройденных тестов,
            //а также заняться ачивментами

            if (tr.answersCorrect < tr.answersMinimum)
            {
				if (score > tr.answersCorrect)
					tr.answersCorrect = score;
				Global.stat.themesRuns[board.theme_num].testsComplete += 1;
//                rpg.Achievement("Тест успешно пройден!\n+30 очков!", 30);
//                rpg.RPG.testsFinished += 1;
//                if (rpg.RPG.testsFinished == 1) rpg.Achievement("Первый пройденный тест!\n+20 очков!", 10);
//                else if (rpg.RPG.testsFinished == 10) rpg.Achievement("Пройдено 10 тестов!\n+100 очков!", 100);
//                else if (rpg.RPG.testsFinished == 25) rpg.Achievement("Пройдено 25 тестов!\n+200 очков!", 200);
//                else if (rpg.RPG.testsFinished == 50) rpg.Achievement("Пройдено 50 тестов!\n+500 очков!", 500);
//                else rpg.Save();
            }
        }
        else
        {
            transform.parent.transform.parent.transform.Find("Board Begin/Conclusion").GetComponent<TextMesh>().text = "Недостаточно верных ответов";
        }
        if ((tr.answersCorrect < score) && (score == tr.answersOverall))
        {
//            rpg.Achievement("Идеальный результат!\n+30 очков!", 30);
        }
        //и независимо от того, засчитан тест или нет, если это пока что лучший результат юзера, его надо запомнить
        if (tr.answersCorrect < score) tr.answersCorrect = score;
        //sp.UpdateThemeStat(board.theme_num + 1);
    }
}
