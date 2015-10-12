using UnityEngine;

public class LectureStart : MonoBehaviour
{
    public Transform room;

    bool lectureStarted = false;

    void OnTriggerEnter(Collider other)
    {
        if (!lectureStarted)
        {
            lectureStarted = true;
            room.transform.Find("Stands").GetComponent<InputScript>().generateLecture(Global.content, Global.theme_num, Global.content_num);
            room.transform.Find("Timer").GetComponent<TimerLecture>().theme_num = Global.theme_num;
            room.transform.Find("Timer").GetComponent<TimerLecture>().lec_num = Global.content_num;
        }
    }
}
