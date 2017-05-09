using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportToScene : MonoBehaviour {

	public string SceneNameRoom;

	public bool active;

	private string currentSceneName;

	void Start () {
		currentSceneName = SceneManager.GetActiveScene().name;
	}
	void OnTriggerEnter()
	{
		Debug.Log("enter portal " + SceneManager.GetActiveScene().name);
		if (active)
		{
			if (currentSceneName != Names.Scenes.World) {
				SaveStat ();
			}
			Global.fromSceneName = currentSceneName;
			PhotonNetwork.LoadLevel(SceneNameRoom);
		}
	}

	void OnTriggerExit()
	{
		TeleportManager.ActivatePortals ();
		SetAchievement ();
	}

	void SaveStat()
	{
		if (Global.stat != null && Global.stat.mode != "guest")
		{
			var s = JsonFx.Json.JsonWriter.Serialize(Global.stat);
			var parameters = new Dictionary<string, string>();
			parameters["s"] = s;
			var httpConnector = GameObject.Find("Bootstrap").AddComponent<HttpConnector>();
			httpConnector.Post(HttpConnector.ServerUrl + HttpConnector.SaveStatisticUrl, parameters, www => {},
				w=>{});
		}
	}

	void SetAchievement()
	{
		if (GameObject.Find ("Bootstrap") != null) {
			RPGParser rpgParser = GameObject.Find ("Bootstrap").GetComponent<RPGParser> ();
			rpgParser.SaveAchievemnt (DataStructures.AchievementTrigger.Teleport, null);
		}
	}
}