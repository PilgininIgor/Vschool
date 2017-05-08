using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
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
        SetCoinsForUser();
    }

    private void SetCoinsForUser()
    {
        httpConnector.Get(HttpConnector.ServerUrl + HttpConnector.GetUserCoinsUrl,
               www =>
               {
                   RPG = new DataStructures.OverallRPG {EXP = int.Parse(www.text)};
               });
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

    public void Achievement(string text, int points)
    {
        achievementText.Add(text);
        achievementPoints.Add(points);
        ShowAchievment(text);
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

    public void SaveAchievemnt(DataStructures.AchievementTrigger trigger, Dictionary<string, object> parameters)
    {
        if (!RPG.ifGuest)
        {
            if (parameters == null)
            {
                parameters = new Dictionary<string, object>();
            }
            httpConnector.Post(HttpConnector.ServerUrl + HttpConnector.SaveGameAchievementUrl, 
                new Dictionary<string, string> { { "triggerValue", trigger.ToString() }, {"parameters", parameters.ToString()} },
                www =>
                {
                    var achievementRuns = JsonReader.Deserialize<DataStructures.GameAchievementRun[]>(www.text);
                    foreach (var achievementRun in achievementRuns.Where(achievementRun => achievementRun.passed && achievementRun.needToShow))
                    {
                        ShowAchievment("Достижение \"" + achievementRun.name + "\" получено!");
                        if (achievementRun.score <= 0)
                        {
                            continue;
                        }
                        ShowCoinsAdded(achievementRun.score);
                        RPG.EXP += achievementRun.score;
                    }
                });
        }
    }
}
