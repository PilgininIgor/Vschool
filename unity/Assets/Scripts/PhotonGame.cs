// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WorkerInGame.cs" company="Exit Games GmbH">
//   Part of: Photon Unity Networking
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.SceneManagement;

public class PhotonGame : Photon.MonoBehaviour
{
	public Transform playerPrefab;

	public string sceneName;

	//avatars
	public Transform Joan;
	public Transform Alexis;
	public Transform Golem;
	public Transform Justin;
	public Transform Vincent;
	public Transform Solder;
	public Transform Mia;
	public Transform Punk;
	public Transform Carl;

	public Vector3 spawnPlace;

	public Texture gear;

	public Transform HubLoadingScreen;

	public string nameOfAvatar;

	public void Awake()
	{
		Init ();
	}

	public void Init()
	{

		//select avatar
		nameOfAvatar = /*"Solder"*/CharacterCust.nameOfAvatar;


		Debug.Log("Avatar is " + nameOfAvatar);

		switch (nameOfAvatar)
		{
		case "Joan": playerPrefab = Joan; break;
		case "Alexis": playerPrefab = Alexis; break;
		case "Justin": playerPrefab = Justin; break;
		case "Vincent": playerPrefab = Vincent; break;
		case "Solder": playerPrefab = Solder; break;
		case "Mia": playerPrefab = Mia; break;
		case "Punk": playerPrefab = Punk; break;
		case "Carl": playerPrefab = Carl; break;
		case "Golem": playerPrefab = Golem; break;
		}



		// in case we started this demo with the wrong scene being active, simply load the menu scene
		if (!PhotonNetwork.connected)
		{
			Application.LoadLevel(PhotonMenu.SceneNameMenu);
			return;
		}
		// we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate

		spawnPlace = TeleportManager.SettingPortals ();

		GameObject instantiatedPlayer = PhotonNetwork.Instantiate(this.playerPrefab.name, spawnPlace, Quaternion.identity, 0);
		//instantiatedPlayer.name = PhotonNetwork.playerName;

		//Camera.main.GetComponent<OrbitCam>().target = instantiatedPlayer.transform;
		//Camera.main.GetComponent<OrbitCam>().player = instantiatedPlayer;

		GameObject.Find("MainCamera").GetComponent<CameraFinC>().target = instantiatedPlayer.transform;
		GameObject.Find("MainCamera").GetComponent<CameraFinC>().player = instantiatedPlayer;

		//GameObject.Find("MainCamera").GetComponent<CNCameraFollow>().targetObject = instantiatedPlayer.transform;

	}

	public void onStart()
	{
		CourseSelection.CourseName course = new CourseSelection.CourseName ()
		{
			id = PlayerPrefs.GetString(PhotonMenu.CourseID),
			name = PlayerPrefs.GetString(PhotonMenu.CourseName)
		};


		if (Application.loadedLevelName == "world") HubLoadingScreen.GetComponent<CourseSelection>().LoadCourseData(course);
	}

	public void OnGUI()
	{
		if (GUI.Button(new Rect(DataStructures.buttonSpace, DataStructures.buttonSpace, DataStructures.buttonSize, DataStructures.buttonSize), gear))
		{
			PhotonNetwork.LeaveRoom();  // we will load the menu level when we successfully left the room
		}
	}

	public void OnMasterClientSwitched(PhotonPlayer player)
	{
		Debug.Log("OnMasterClientSwitched: " + player);

		string message;
		InRoomChat chatComponent = GetComponent<InRoomChat>();  // if we find a InRoomChat component, we print out a short message

		if (chatComponent != null)
		{
			// to check if this client is the new master...
			if (player.isLocal)
			{
				message = "You are Master Client now.";
			}
			else
			{
				message = player.name + " is Master Client now.";
			}


			chatComponent.AddLine(message); // the Chat method is a RPC. as we don't want to send an RPC and neither create a PhotonMessageInfo, lets call AddLine()
		}
	}

	public void OnLeftRoom()
	{
		Debug.Log("OnLeftRoom (local)");

		// back to main menu        
		Application.LoadLevel(PhotonMenu.SceneNameMenu);
	}

	public void OnDisconnectedFromPhoton()
	{
		Debug.Log("OnDisconnectedFromPhoton");

		// back to main menu        
		Application.LoadLevel(PhotonMenu.SceneNameMenu);
	}

	public void OnPhotonInstantiate(PhotonMessageInfo info)
	{
		Debug.Log("OnPhotonInstantiate " + info.sender);    // you could use this info to store this or react
	}

	public void OnPhotonPlayerConnected(PhotonPlayer player)
	{
		Debug.Log("OnPhotonPlayerConnected: " + player);
	}

	public void OnPhotonPlayerDisconnected(PhotonPlayer player)
	{
		Debug.Log("OnPlayerDisconneced: " + player);
	}

	public void OnFailedToConnectToPhoton()
	{
		Debug.Log("OnFailedToConnectToPhoton");

		// back to main menu        
		Application.LoadLevel(PhotonMenu.SceneNameMenu);
	}

}
