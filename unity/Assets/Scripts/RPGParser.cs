using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using JsonFx.Json;
using UnityEngine.UI;

public class RPGParser : MonoBehaviour
{
    private string JSONTestString = "{" +
    "\"ifGuest\":false,\"username\":\"Student1\"," +
    "\"EXP\":150," +
    "\"facultyStands_Seen\":false," + "\"facultyStands_Finish\":false," +
    "\"historyStand_Seen\":false," + "\"historyStand_Finish\":false," +
    "\"scienceStand_Seen\":false," + "\"scienceStand_Finish\":false," +
    "\"staffStand_Seen\":false," + "\"staffStand_Finish\":false," +
    "\"logotypeJump\":false," + "\"tableJump\":false," + "\"terminalJump\":false," +
    "\"ladderJump_First\":false," + "\"ladderJump_All\":false," + "\"letThereBeLight\":false," +
    "\"plantJump_First\":false," + "\"plantJump_Second\":false," + "\"barrelRoll\":false," +
    "\"firstVisitLecture\":false," + "\"firstVisitTest\":false," +
    "\"teleportations\":0," + "\"paragraphsSeen\":1," + "\"testsFinished\":0" +
"}";

    public DataStructures.OverallRPG RPG;
    public DataStructures.GameAchievement[] Achievements;

    public Font helvetica;
    private bool SkinSet = false;
    private bool UNwidthCalculated = false;
    private float UNwidth;

    private bool displayAchievement = false;
    private int displayAchievement_stage;
    private float time_to_show;
    private double pos_x = 0;
    private double pos_y = 0;

    private List<string> achievementText = new List<string>();
    private List<int> achievementPoints = new List<int>();
    private string txt;
    private int pnt;
    private int count = 0;
    private GameObject Player;
    string LBL = "Очки опыта";
    private string nameOfAvatar;

    private HttpConnector httpConnector;

    public Text coinsText;
    public Text coinsAddedText;

    public GameObject awardPopup;
    public RawImage awardImage;
    public Text awardText;
    public Texture texture;

    // Use this for initialization
    void Start()
    {
        httpConnector = GetComponent<HttpConnector>();
        RoleSystemSet(JSONTestString);
        LoadGameAchievements();
    }

    private void ShowAchievment(string text, Texture image = null)
    {
        awardPopup.GetComponent<CanvasGroup>().alpha = 1;
        awardText.text = text;
        if (image != null)
        {
            awardImage.GetComponent<CanvasGroup>().alpha = 1;
            awardImage.texture = image;
        }
        StartCoroutine(HideAchievment());
    }

    private IEnumerator HideAchievment()
    {
        yield return new WaitForSeconds(5);
        awardPopup.GetComponent<CanvasGroup>().alpha = 0;
    }

    private void ShowCoinsAdded(int coins)
    {
        coinsAddedText.GetComponent<CanvasGroup>().alpha = 1;
        coinsAddedText.text = "+" + coins;
        StartCoroutine(HideCoinsAdded());
    }

    private IEnumerator HideCoinsAdded()
    {
        var canvasGroup = coinsAddedText.GetComponent<CanvasGroup>();
        while (canvasGroup.alpha >= 0)
        {
            canvasGroup.alpha -= 0.2f;
            yield return null;
        }
    }

    private void LoadGameAchievements()
    {
        httpConnector.Get(HttpConnector.ServerUrl + HttpConnector.GetGameAchievementsUrl, www =>
        {
            Achievements = JsonReader.Deserialize<DataStructures.GameAchievement[]>(www.text);
        });
    }

    private void RoleSystemSet(string json)
    {
        RPG = JsonReader.Deserialize<DataStructures.OverallRPG>(json);
    }

    // Update is called once per frame
    void Update()
    {
        if (RPG != null)
            coinsText.text = RPG.EXP.ToString();
    }

    void OnGui()
    {
        if (!SkinSet)
        {
            //GUI.skin.box.font = helvetica; GUI.skin.box.fontSize = 12; GUI.skin.box.fontStyle = FontStyle.BoldAndItalic;
            //GUI.skin.label.font = helvetica; GUI.skin.label.fontStyle = FontStyle.Bold;
            //GUI.skin.label.fontSize = 12; GUI.skin.label.normal.textColor = Color(0.835, 0.929, 1);
            //GUI.skin.label.alignment = TextAnchor.MiddleCenter;
            //SkinSet = true;
        }
        if (displayAchievement)
        {
            if (displayAchievement_stage == 0)
            {
                txt = achievementText[0]; pnt = achievementPoints[0];
                displayAchievement_stage = 1;
            }
            if (displayAchievement_stage == 1)
            {
                GUI.skin.label.fontSize++;
                GUI.Label(new Rect(0, Screen.height / 4 - 25, Screen.width, 200), txt);
                if (GUI.skin.label.fontSize >= 42) { displayAchievement_stage++; time_to_show = Time.timeSinceLevelLoad; }
            }
            else if (displayAchievement_stage == 2)
            {
                GUI.Label(new Rect(0, Screen.height / 4 - 25, Screen.width, 200), txt);
                if (Time.timeSinceLevelLoad - time_to_show > 3) displayAchievement_stage++;
            }
            else if (displayAchievement_stage == 3)
            {
                pos_x += Screen.width / 50.0;
                pos_y += Screen.height * 0.75 / 50;
                GUI.Label(new Rect(0, (float)(Screen.height / 4 - 25 + pos_y), (float)(Screen.width - pos_x), 200), txt);
                GUI.skin.label.fontSize--;
                if (GUI.skin.label.fontSize <= 0)
                {
                    RPG.EXP += pnt;
                    Save();
                    GUI.skin.label.fontSize = 0; pos_x = 0; pos_y = 0;
                    count--;
                    if (count == 0) displayAchievement = false;
                    else
                    {
                        displayAchievement_stage = 0;
                        for (var i = 0; i < count; i++)
                        {
                            achievementText[i] = achievementText[i + 1];
                            achievementPoints[i] = achievementPoints[i + 1];
                        }
                    }
                }
            }
        }
    }

    public void Achievement(string text, int points)
    {
        count++;
        achievementText.Add(text);
        achievementPoints.Add(points);
        if (!displayAchievement)
        {
            displayAchievement = true;
            displayAchievement_stage = 0;
        }
    }

    public void Save()
    {
        if (!RPG.ifGuest)
        {
            var s = JsonWriter.Serialize(RPG);
            var parameters = new Dictionary<string, string>();
            parameters["s"] = s;
            httpConnector.Post(HttpConnector.ServerUrl + HttpConnector.UnitySaveRpgUrl, parameters, www => { });
        }
    }

    public void SaveAchievemnt(string guid)
    {
        if (!RPG.ifGuest)
        {
            httpConnector.Post(HttpConnector.ServerUrl + HttpConnector.SaveGameAchievementUrl, new Dictionary<string, string> { { "achievementId", guid } },
                www =>
                {
                    var achievementRuns = JsonReader.Deserialize<DataStructures.GameAchievementRun[]>(www.text);
                    foreach (var achievementRun in achievementRuns)
                    {
                        if (/*achievementRun.passed && achievementRun.needToShow*/true)
                        {
                            ShowAchievment("Достижение \"" + achievementRun.name + "\" получено!");
                            if (achievementRun.score > 0)
                            {
                                ShowCoinsAdded(achievementRun.score);
                                RPG.EXP += achievementRun.score;
                            }
                        }
                    }
                });
        }
    }
}
