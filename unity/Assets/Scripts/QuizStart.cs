using UnityEngine;

public class QuizStart : MonoBehaviour
{
    public Transform room;

    bool quizStarted = false;

    void OnTriggerEnter(Collider other)
    {
        if (!quizStarted)
        {
            quizStarted = true;
            room.transform.Find("Board To Move/BoardGroup").GetComponent<Board>().generateTest(Global.content, Global.theme_num, Global.content_num);
            room.transform.Find("Timer").GetComponent<TimerTest>().theme_num = Global.theme_num;
            room.transform.Find("Timer").GetComponent<TimerTest>().test_num = Global.content_num;
        }
    }
}
