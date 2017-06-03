using UnityEngine;
using System.Collections;

public class CharacterCust : MonoBehaviour
{
    //positions
    //float y = 2.0f, z = 31.0f, xLeft = 20.0f, xMiddle = 21.5f, xRigth = 23.0f;
    Vector3 leftPosition = new Vector3(20.0f, 2.0f, 31.0f);
    Vector3 middlePosition = new Vector3(21.5f, 2.0f, 31.0f);
    Vector3 rigthPosition = new Vector3(23.0f, 2.0f, 31.0f);

    bool buttonsIsVisible = true;

    //avatars
    public static string nameOfAvatar;
    public GameObject AvatarNameText;

    public GameObject Observation;

    public GUIStyle guiStyle;

    int curCharacter = 0;
    int curEffect = 0;
    bool closerCamera = false;

    GameObject[] characters;
    GameObject[] effects;

    // Use this for initialization
    void Start()
    {
        characters = GameObject.FindGameObjectsWithTag("Avatar");
        effects = GameObject.FindGameObjectsWithTag("Effect");
        Debug.Log("Effects size:" + effects.Length);
        ChangeCharecters(curCharacter);
        ChangeEffects(curEffect);
    }

    void ZoomButton(int wRegularButton, int wBigButton, int hUnit)
    {
        GUILayout.BeginHorizontal();
        if (!closerCamera)
        {
            if (GUILayout.Button(Strings.Get("Look Around"), GUILayout.Width(wRegularButton), GUILayout.Height(hUnit)))
            {
                Observation.transform.Find("MainCamera").localPosition = new Vector3(2, 3, -3);
                Observation.transform.Find("MainCamera").eulerAngles = new Vector3(0, 325, 0);
                closerCamera = true;
            }
        }
        else
        {
            Observation.transform.Find("MainCamera").RotateAround(
                Observation.transform.position - new Vector3(0, 0, 0.8f),
                Vector3.up,
                -20 * Time.deltaTime
            );
            if (GUILayout.Button(Strings.Get("Stop Watch"), GUILayout.Width(wRegularButton), GUILayout.Height(hUnit)))
            {
                Observation.transform.Find("MainCamera").localPosition = new Vector3(2, 3, -3);
                Observation.transform.Find("MainCamera").eulerAngles = new Vector3(0, 325, 0);
                closerCamera = false;
            }
        }
        GUILayout.EndHorizontal();
    }

    void commonButtons(int wRegularButton, int wBigButton, int hUnit)
    {
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("<", GUILayout.Width(wRegularButton), GUILayout.Height(hUnit)))
        {
            curCharacter--;
            if (curCharacter < 0)
                curCharacter = characters.Length - 1;
            ChangeCharecters(curCharacter);
        }

        if (GUILayout.Button(Strings.Get("Go"), GUILayout.Width(wRegularButton), GUILayout.Height(hUnit)))
        {
            nameOfAvatar = characters[curCharacter].name;
            Debug.Log("Selected avatar " + characters[curCharacter].name);
            var menu = GameObject.Find("_Customization").AddComponent<PhotonMenu>();
            menu.listStyle = guiStyle;
            buttonsIsVisible = false;
			Application.LoadLevel(Names.Scenes.World);
        }
        if (GUILayout.Button(">", GUILayout.Width(wRegularButton), GUILayout.Height(hUnit)))
        {
            curCharacter++;
            if (curCharacter >= characters.Length)
                curCharacter = 0;
            ChangeCharecters(curCharacter);
        }
        GUILayout.EndHorizontal();
    }

    void OnGUI()
    {
        if (buttonsIsVisible)
            using (var skin = new DefaultSkin())
            {
                int hUnit = Mathf.RoundToInt(Screen.height * DefaultSkin.LayoutScale);
                int wUnit = Mathf.RoundToInt(Screen.width * DefaultSkin.LayoutScale);

                int wRegularButton = wUnit;
                int wBigButton = wUnit * 2;
                int wHugeButton = wUnit * 3;

                int blockWidth = wUnit * 3;
                int blockHeight = hUnit * 3;
                int x = (Screen.width / 2) - (blockWidth / 2);
                int y = (Screen.height / 2) - (blockHeight / 2);
                hUnit /= 2;

                GUILayout.BeginArea(new Rect(x, y, blockWidth + 15, blockHeight));

                commonButtons(wRegularButton, wBigButton, hUnit);

                GUILayout.EndArea();

                GUILayout.BeginArea(new Rect(x + wRegularButton + 5, y - hUnit - 5, blockWidth, blockHeight));

                ZoomButton(wRegularButton, wBigButton, hUnit);

                GUILayout.EndArea();


            }
    }


    void ChangeCharecters(int curCharacter)
    {
        Debug.Log(characters[curCharacter].name + " # " + curCharacter);
        for (int i = 0; i < characters.Length; i++)
            characters[i].SetActive(false);
        characters[curCharacter].SetActive(true);
        AvatarNameText.GetComponent<GUIText>().text = characters[curCharacter].name;
    }

    void ChangeEffects(int curEffect)
    {
        foreach (GameObject t in effects)
            t.SetActive(false);
        if (curEffect < effects.Length)
            effects[curEffect].SetActive(true);
    }
}