// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WorkerMenu.cs" company="Exit Games GmbH">
//   Part of: Photon Unity Networking
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using JsonFx.Json;
using UnityEngine;
using Random = UnityEngine.Random;

public class PhotonMenu : MonoBehaviour
{

    private string roomName = "Room";

    private Vector2 scrollPos = Vector2.zero;

    private bool connectFailed = false;

    public static readonly string SceneNameMenu = "customization";

    public static readonly string SceneNameGame = "world";

    public const string PlayerName = "playerName";
    public const string CourseID = "courseID";
	public const string CourseName = "courseName";

    private string userName = Strings.Get("Guest");

    private const int dlgWidth = 450, dlgheight = 300, dlgPadding = 5;

    private HttpConnector httpConnector;

    private List<CourseSelection.CourseName> coursesNames;
    private bool isDataLoaded;
    private GUIContent[] comboBoxList;
    private ComboBox comboBoxControl = new ComboBox();
    public GUIStyle listStyle;

    public void Awake()
    {
        httpConnector = GameObject.Find("_Customization").GetComponent<HttpConnector>();
        //TODO get name from server
        userName = "Student";
        GetUserFromServer();
        LoadCoursesList();

        // this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
        PhotonNetwork.automaticallySyncScene = true;

        // the following line checks if this client was just created (and not yet online). if so, we connect
        if (PhotonNetwork.connectionStateDetailed == PeerState.PeerCreated)
        {
            // Connect to the photon master-server. We use the settings saved in PhotonServerSettings (a .asset file in this project)
            PhotonNetwork.ConnectUsingSettings("1.0");
        }

        // generate a name for this player, if none is assigned yet
        if (String.IsNullOrEmpty(PhotonNetwork.playerName))
        {
            PhotonNetwork.playerName = userName + " " + Random.Range(1, 9999);
        }

        // if you wanted more debug out, turn this on:
        // PhotonNetwork.logLevel = NetworkLogLevel.Full;
    }

    void GetUserFromServer()
    {
        httpConnector.Get(HttpConnector.ServerUrl + HttpConnector.GetUsernameUrl, www =>
        {
            PhotonNetwork.playerName = JsonReader.Deserialize<String>(www.text);
        });
    }

    public void LoadCoursesList()
    {
        httpConnector.Get(HttpConnector.ServerUrl + HttpConnector.UnityListUrl, www =>
        {
            var res = JsonReader.Deserialize<CourseSelection.CoursesNamesList>(www.text);
            coursesNames = res.coursesNames;
            comboBoxList = coursesNames.Select(c => new GUIContent(c.name)).ToArray();
            isDataLoaded = true;
        });
    }

    private void UpdateRoomName()
    {
        roomName = comboBoxList[comboBoxControl.GetSelectedItemIndex()].text;
    }

    public void OnGUI()
    {
        GUI.skin.box.fontStyle = FontStyle.Bold;
        GUI.Box(new Rect((Screen.width - dlgWidth) / 2 - dlgPadding, (Screen.height - dlgheight) / 2 - dlgPadding, dlgWidth + dlgPadding, dlgheight + dlgPadding), Strings.Get("Join or Create a Course"));
        GUILayout.BeginArea(new Rect((Screen.width - dlgWidth) / 2, (Screen.height - dlgheight) / 2, dlgWidth, dlgheight));

        GUILayout.FlexibleSpace();

        if (!PhotonNetwork.connected)
        {
            GUILayout.BeginHorizontal();
            if (PhotonNetwork.connecting)
            {
                GUILayout.Label("Connecting to: " + PhotonNetwork.ServerAddress, GUILayout.Width(dlgWidth * 3 / 4));
            }
            else
            {
                GUILayout.Label("Not connected. Check console output. Detailed connection state: " + PhotonNetwork.connectionStateDetailed + " Server: " + PhotonNetwork.ServerAddress, GUILayout.Width(dlgWidth * 3 / 4));
            }

            if (connectFailed)
            {
                GUILayout.Label("Connection failed. Check setup and use Setup Wizard to fix configuration.", GUILayout.Width(dlgWidth * 3 / 4));
                GUILayout.Label(String.Format("Server: {0}", new object[] { PhotonNetwork.ServerAddress }), GUILayout.Width(dlgWidth * 3 / 4));
                GUILayout.Label("AppId: " + PhotonNetwork.PhotonServerSettings.AppID, GUILayout.Width(dlgWidth * 3 / 4));

                if (GUILayout.Button(Strings.Get("Try Again"), GUILayout.Width(dlgWidth / 4)))
                {
                    connectFailed = false;
                    PhotonNetwork.ConnectUsingSettings("1.0");
                }
            }
            GUILayout.EndHorizontal();
        }
        else
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(Strings.Get("User name:"), GUILayout.Width(dlgWidth / 4));
            PhotonNetwork.playerName = GUILayout.TextField(PhotonNetwork.playerName);
            if (GUI.changed)
            {
                // Save name
                PlayerPrefs.SetString(PlayerName, PhotonNetwork.playerName);
                UpdateRoomName();
            }
            GUILayout.EndHorizontal();

            GUILayout.FlexibleSpace();

            // Join room by title
            GUILayout.BeginHorizontal();
            GUILayout.Label(Strings.Get("Course name:"), GUILayout.Width(dlgWidth / 4));
            Rect r = GUILayoutUtility.GetLastRect();
            if (!isDataLoaded)
            {
                GUILayout.Label(Strings.Get("Loading"), GUILayout.Width(dlgWidth / 4));
            }
            GUILayout.EndHorizontal();

            GUILayout.FlexibleSpace();
            // Create a room (fails if exist!)
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button(Strings.Get("Create Course"), GUILayout.Width(dlgWidth / 4)))
            {
                UpdateRoomName();
                PhotonNetwork.CreateRoom(roomName, new RoomOptions { maxPlayers = 10 }, null);
            }
            GUILayout.FlexibleSpace();
            if (GUILayout.Button(Strings.Get("Join Course"), GUILayout.Width(dlgWidth / 4)))
            {
                UpdateRoomName();
                PhotonNetwork.JoinRoom(roomName);
            }
            GUILayout.FlexibleSpace();
            if (GUILayout.Button(Strings.Get("Join Random"), GUILayout.Width(dlgWidth / 4)))
            {
                PhotonNetwork.JoinRandomRoom();
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            GUILayout.FlexibleSpace();

            GUILayout.BeginHorizontal();
            GUILayout.Label(PhotonNetwork.countOfPlayers + Strings.Get(" users are online in ") + PhotonNetwork.countOfRooms + Strings.Get(" courses."));
            GUILayout.EndHorizontal();

            GUILayout.FlexibleSpace();
            if (PhotonNetwork.GetRoomList().Length == 0)
            {
                GUILayout.Label(Strings.Get("Currently no games are available."));
                GUILayout.Label(Strings.Get("Courses will be listed here, when they become available."));
            }
            else
            {
                GUILayout.Label(PhotonNetwork.GetRoomList().Length + Strings.Get(" currently available. Join either:"));

                // Room listing: simply call GetRoomList: no need to fetch/poll whatever!
                scrollPos = GUILayout.BeginScrollView(scrollPos);
                foreach (RoomInfo roomInfo in PhotonNetwork.GetRoomList())
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Label(roomInfo.name + " " + roomInfo.playerCount + "/" + roomInfo.maxPlayers);
                    if (GUILayout.Button(Strings.Get("Join")))
                    {
                        PhotonNetwork.JoinRoom(roomInfo.name);
                    }

                    GUILayout.EndHorizontal();
                }

                GUILayout.EndScrollView();
            }
            if (isDataLoaded)
            {
                int selectedItemIndex = comboBoxControl.GetSelectedItemIndex();
                selectedItemIndex = comboBoxControl.List(new Rect(r.xMax + dlgPadding, r.yMin, dlgWidth / 2, 20), comboBoxList[selectedItemIndex].text, comboBoxList, listStyle);
            }
        }

        GUILayout.EndArea();
    }

    // We have two options here: we either joined(by title, list or random) or created a room.
    public void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");
    }

    public void OnCreatedRoom()
    {
        Debug.Log("OnCreatedRoom");
        //PhotonNetwork.isMessageQueueRunning = false;
        PlayerPrefs.SetString(CourseID, coursesNames[comboBoxControl.GetSelectedItemIndex()].id);
		PlayerPrefs.SetString(CourseName, coursesNames[comboBoxControl.GetSelectedItemIndex()].name);
        PhotonNetwork.LoadLevel(SceneNameGame);
    }

    public void OnDisconnectedFromPhoton()
    {
        Debug.Log("Disconnected from Photon.");
    }

    public void OnFailedToConnectToPhoton(object parameters)
    {
        this.connectFailed = true;
        Debug.Log("OnFailedToConnectToPhoton. StatusCode: " + parameters + " ServerAddress: " + PhotonNetwork.networkingPeer.ServerAddress);
    }
}
