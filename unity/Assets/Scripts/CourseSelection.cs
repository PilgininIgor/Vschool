using UnityEngine;
using System.Collections.Generic;

public class CourseSelection : MonoBehaviour
{

    class CoursesNamesList
    {
        public List<CourseName> coursesNames;
    }

    class CourseName
    {
        public string id;
        public string name;
    }

    private string JSONTestString = "{\"coursesNames\":[" +
        "{\"id\":\"47942238-8168-404c-965e-4649b2a0278b\", \"name\":\"Информатика\"}," +
        "{\"id\":\"be549932-1844-4d2f-bded-f4b9aac61f97\", \"name\":\"Математика\"}," +
        "{\"id\":\"47942238-8168-404c-965e-111111111111\", \"name\":\"Физика\"}" +
        //"{\"id\":\"47942238-8168-404c-965e-222222222222\", \"name\":\"Больше физики\"}"+
    "]}";

    const string LBL1 = "Загрузка...";
    const string LBL2 = "A / стрелка влево - предыдущий курс";
    const string LBL3 = "D / стрелка вправо - следующий курс";
    const string LBL4 = "Enter - загрузка";

    Camera GuiCam, MainCam, StandCam;
    GameObject Player;
    Texture magnifier, arrow, select;
    Font helvetica;

    Material NewPipe, NewScreen;
    SignalSender dieSignals;

    List<CourseName> cl; int i;
    private bool hint_visible = false;
    private bool escape_visible = false;
    private bool data_loaded = false;
    private int ButtonSize = 100;

    void Start()
    {
        StandCam.enabled = false; StandCam.GetComponent<AudioListener>().enabled = false;
    }

    void ZoomIn()
    {

        Player = GameObject.Find("MainCamera").GetComponent<OrbitCam>().player;
        hint_visible = false;
        Player.SetActive(false);
        GuiCam.enabled = false;
        MainCam.enabled = false;
        MainCam.GetComponent<AudioListener>().enabled = false;
        StandCam.enabled = true; StandCam.GetComponent<AudioListener>().enabled = true;
        transform.parent.transform.Find("Menu").gameObject.SetActive(true);
        if (!data_loaded)
        {
            transform.parent.transform.Find("Menu/TextMain").GetComponent<TextMesh>().text = LBL1;
            transform.parent.transform.Find("Menu/TextCounter").GetComponent<TextMesh>().text = "";

            //БОЛЬШОЙ РУБИЛЬНИК
            //CourseDisplay(JSONTestString);
            var httpConnector = new HttpConnector();
            httpConnector.LoadCoursesList();
            //Application.ExternalCall("LoadCoursesList");

        }
        else
        {
            escape_visible = true;
        }
    }

    void CourseDisplay(string JSONStringFromServer)
    {
        CoursesNamesList res = JsonFx.Json.JsonReader.Deserialize<CoursesNamesList>(JSONStringFromServer);
        cl = res.coursesNames;
        i = 1;
        transform.parent.transform.Find("Menu/TextMain").GetComponent<TextMesh>().text = cl[i - 1].name;
        transform.parent.transform.Find("Menu/TextCounter").GetComponent<TextMesh>().text = "1/" + cl.Count.ToString();
        data_loaded = true; escape_visible = true;
    }

    void ZoomOut()
    {
        escape_visible = false;
        Player.SetActive(true);
        GuiCam.enabled = true;
        MainCam.enabled = true; MainCam.GetComponent<AudioListener>().enabled = true;
        StandCam.enabled = false; StandCam.GetComponent<AudioListener>().enabled = false;
        transform.parent.transform.Find("Menu").gameObject.SetActive(false);
    }

    void OnGUI()
    {
        if (hint_visible)
        {
            if (GUI.Button(new Rect(10, 10, ButtonSize, ButtonSize), magnifier)) ZoomIn();
        }
        if (escape_visible)
        {
            if (GUI.Button(new Rect(10, 10, ButtonSize, ButtonSize), arrow)) ZoomOut();
            if (GUI.Button(new Rect(Screen.width - ButtonSize, 10, ButtonSize, ButtonSize), select))
            {
                transform.parent.transform.Find("Menu/TextMain").GetComponent<TextMesh>().text = LBL1;
                transform.parent.transform.Find("Menu/TextCounter").GetComponent<TextMesh>().text = "";

                //БОЛЬШОЙ РУБИЛЬНИК
                //Application.ExternalCall("LoadCourseData", cl[i-1].id);			
                LoadCourseData(cl[i - 1].id);

                dieSignals.SendSignals(this);
                this.renderer.material = NewScreen;
                transform.parent.transform.Find("UnlockPipe").renderer.material = NewPipe;
                escape_visible = false; data_loaded = false; //ZoomOut();
            }
            var hUnit = Mathf.RoundToInt(Screen.height * DefaultSkin.LayoutScale);
            var wUnit = Mathf.RoundToInt(Screen.width * DefaultSkin.LayoutScale);
            var blockWidth = wUnit * 3 + 30;
            var blockHeight = hUnit * 3;
            var x = (Screen.width / 2) - (blockWidth / 2);
            var y = (Screen.height / 2) - (blockHeight / 2);
            wUnit /= 4;
            if (GUI.Button(new Rect(x, y, wUnit, hUnit), "<"))
            {
                i--; if (i <= 0) i = cl.Count;
                transform.parent.transform.Find("Menu/TextMain").GetComponent<TextMesh>().text = cl[i - 1].name;
                transform.parent.transform.Find("Menu/TextCounter").GetComponent<TextMesh>().text = i.ToString() + "/" + cl.Count.ToString();
            }

            if (GUI.Button(new Rect(x + blockWidth, y, wUnit, hUnit), ">"))
            {
                i++; if (i > cl.Count) i = 1;
                transform.parent.transform.Find("Menu/TextMain").GetComponent<TextMesh>().text = cl[i - 1].name;
                transform.parent.transform.Find("Menu/TextCounter").GetComponent<TextMesh>().text = i.ToString() + "/" + cl.Count.ToString();
            }
        }
    }

    void OnTriggerEnter(Collider other) { hint_visible = true; }
    void OnTriggerExit(Collider other) { hint_visible = false; }

    void LoadCourseData(string id)
    {
        var httpConnector = new HttpConnector();
        httpConnector.LoadCourseData(id);
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && escape_visible)
        {
            i--; if (i <= 0) i = cl.Count;
            transform.parent.transform.Find("Menu/TextMain").GetComponent<TextMesh>().text = cl[i - 1].name;
            transform.parent.transform.Find("Menu/TextCounter").GetComponent<TextMesh>().text = i.ToString() + "/" + cl.Count.ToString();
        }

        if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && escape_visible)
        {
            i++; if (i > cl.Count) i = 1;
            transform.parent.transform.Find("Menu/TextMain").GetComponent<TextMesh>().text = cl[i - 1].name;
            transform.parent.transform.Find("Menu/TextCounter").GetComponent<TextMesh>().text = i.ToString() + "/" + cl.Count.ToString();
        }

        if (Input.GetKeyDown(KeyCode.Return) && escape_visible)
        {
            transform.parent.transform.Find("Menu/TextMain").GetComponent<TextMesh>().text = LBL1;
            transform.parent.transform.Find("Menu/TextCounter").GetComponent<TextMesh>().text = "";

            //БОЛЬШОЙ РУБИЛЬНИК
            //Application.ExternalCall("LoadCourseData", cl[i-1].id);
            LoadCourseData(cl[i - 1].id);

            dieSignals.SendSignals(this);
            this.renderer.material = NewScreen;
            transform.parent.transform.Find("UnlockPipe").renderer.material = NewPipe;
            escape_visible = false; data_loaded = false; //ZoomOut();
        }
    }
}
