using UnityEngine;

public class TextAdventureManager : MonoBehaviour
{

    Transform player;
    MoodBox[] playableMoodBoxes;

    float timePerChar = 0.125f;

    private int currentMoodBox = 0, textAnimation = 0;
    private float timer = 0.0f;
    private Vector3 camOffset = Vector3.zero;

    void Start()
    {
        if (!player)
            player = GameObject.FindWithTag("Player").transform;

        var leftIcon = new GameObject("Left Arrow", typeof(GUIText));
        var rightIcon = new GameObject("Right Arrow", typeof(GUIText));

#if UNITY_IPHONE || UNITY_ANDROID
        leftIcon.guiText.text = "<";
#else
	leftIcon.GetComponent<GUIText>().text = "< backspace";		
#endif

        leftIcon.GetComponent<GUIText>().font = GetComponent<GUIText>().font;
        leftIcon.GetComponent<GUIText>().material = GetComponent<GUIText>().material;
        leftIcon.GetComponent<GUIText>().anchor = TextAnchor.UpperLeft;
        leftIcon.gameObject.layer = (LayerMask.NameToLayer("Adventure"));

        leftIcon.transform.position = new Vector3(0.01f, 0.1f, 0);

#if UNITY_IPHONE || UNITY_ANDROID
        rightIcon.guiText.text = ">";
#else
	rightIcon.GetComponent<GUIText>().text = "space >";		
#endif
        rightIcon.GetComponent<GUIText>().font = GetComponent<GUIText>().font;
        rightIcon.GetComponent<GUIText>().material = GetComponent<GUIText>().material;
        rightIcon.GetComponent<GUIText>().anchor = TextAnchor.UpperRight;
        rightIcon.gameObject.layer = (LayerMask.NameToLayer("Adventure"));

        rightIcon.transform.position = new Vector3(0.99f, 0.1f, 0);
    }

    void OnEnable()
    {
        textAnimation = 0;
        timer = timePerChar;

        camOffset = Camera.main.transform.position - player.position;

        BeamToBox(currentMoodBox);

        if (player)
        {
            var ctrler = player.GetComponent<PlayerMoveController>();
            ctrler.enabled = false;
        }

        GetComponent<GUIText>().enabled = true;
    }

    void OnDisable()
    {
        // and back to normal player control

        if (player)
        {
            var ctrler = player.GetComponent<PlayerMoveController>();
            ctrler.enabled = true;
        }

        GetComponent<GUIText>().enabled = false;
    }

    void Update()
    {
        GetComponent<GUIText>().text = "AngryBots \n \n" + playableMoodBoxes[currentMoodBox].data.adventureString.Substring(0, textAnimation);

        Debug.Log(GetComponent<GUIText>().text);

        if (textAnimation >= playableMoodBoxes[currentMoodBox].data.adventureString.Length)
        {

        }
        else
        {
            timer -= Time.deltaTime;
            if (timer <= 0.0f)
            {
                textAnimation++;
                timer = timePerChar;
            }
        }

        CheckInput();
    }

    void BeamToBox(int index)
    {
        if (index > playableMoodBoxes.Length)
            return;

        player.position = playableMoodBoxes[index].transform.position;
        Camera.main.transform.position = player.position + camOffset;
        textAnimation = 0;
        timer = timePerChar;
    }

    void CheckInput()
    {
        int input = 0;

#if UNITY_IPHONE || UNITY_ANDROID
        foreach (var touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
            {
                input = touch.position.x < Screen.width / 2 ? -1 : 1;
            }
        }
#else
		if (Input.GetKeyUp (KeyCode.Space))
			input = 1;
		else if (Input.GetKeyUp (KeyCode.Backspace))
			input = -1;
#endif

        if (input != 0)
        {
            if (textAnimation < playableMoodBoxes[currentMoodBox].data.adventureString.Length)
            {
                textAnimation = playableMoodBoxes[currentMoodBox].data.adventureString.Length;
                input = 0;
            }
        }

        if (input != 0)
        {
            if (input >= 0 || ((currentMoodBox - playableMoodBoxes.Length) != -1 && currentMoodBox != 0))
            {
                currentMoodBox = (input + currentMoodBox) % playableMoodBoxes.Length;
                BeamToBox(currentMoodBox);
            }
        }
    }
}
