//не забудьте добавить box collider к плейну и настроить его размеры
//(они и будут областью, при захождении в которому всплывет кнопка с лупой)
//а еще назначить в инспекторе слой "Ignore Raycast" этому плейну и персонажу,
//чтобы ни тот, ни другой не загораживали коллайдеры кнопок "Назад/Далее" и фоток
//и не мешали по ним кликать
using UnityEngine;

public class CameraChange : MonoBehaviour
{

    public Camera MainCam, StandCam;
    public GameObject Player, HallwayGroup;
    public Texture magnifier, arrow;

    public string addInfo = "";

    public bool hint_visible = false, escape_visible = false;

    // Use this for initialization
    void Start()
    {
        StandCam.enabled = false;
    }

    void setIsBound(bool isBound)
    {
        Player = GameObject.Find("MainCamera").GetComponent<OrbitCam>().player;

        Player.SetActive(!isBound);

        MainCam.enabled = !isBound;
        MainCam.GetComponent<AudioListener>().enabled = !isBound;

        StandCam.enabled = isBound;
        StandCam.GetComponent<AudioListener>().enabled = isBound;

        hint_visible = !isBound;
        escape_visible = isBound;

        if (!isBound)
        {
            HallwayGroup.GetComponent<HallwayAchievements>().Check(transform.parent.name, addInfo);
        }
    }

    void ZoomIn()
    {
        setIsBound(true);
    }

    void ZoomOut()
    {
        setIsBound(false);
    }

    void OnGUI()
    {
        if (hint_visible)
            if (GUI.Button(new Rect(DataStructures.buttonSize + 2 * 10, 10, DataStructures.buttonSize, DataStructures.buttonSize), magnifier))
                ZoomIn();

        if (escape_visible)
            if (GUI.Button(new Rect(DataStructures.buttonSize + 2 * 10, 10, DataStructures.buttonSize, DataStructures.buttonSize), arrow))
                ZoomOut();
    }

    void OnTriggerEnter(Collider other)
    {
        hint_visible = true;
    }

    void OnTriggerExit(Collider other)
    {
        hint_visible = false;
    }
}
