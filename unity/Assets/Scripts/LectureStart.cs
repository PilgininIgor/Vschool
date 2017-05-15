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

			GameObject avatar = GameObject.FindWithTag("Avatar");

			for (int i = 1; i <= 7; i++)
				room.transform.Find ("Stands/" + i + "/StandGroup").GetComponent<Collision> ().player = avatar;
        }
    }
}
