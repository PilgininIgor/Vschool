using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportManager : MonoBehaviour {

	public static Vector3 SettingPortals() {
		Vector3 spawnPlace = new Vector3 ();
		Debug.Log ("From scene is '" + Global.fromSceneName + "'.");
		string currentSceneName = SceneManager.GetActiveScene().name;
		string fromSceneName = Global.fromSceneName;
		switch (currentSceneName) {
		case Names.Scenes.World: 
			if (fromSceneName == Names.Scenes.CourseRoom || fromSceneName == Names.Scenes.ThemeRoom) {
				spawnPlace = GameObject.Find (Names.SpawnPlaces.Course).transform.position;
				GameObject.Find ("MonitorToCourse/Text").GetComponent<TextMesh> ().text = Global.courseData.name;
			} else {
				spawnPlace = GameObject.Find (Names.SpawnPlaces.Start).transform.position;
			}
			break;
		case Names.Scenes.CourseRoom:
			if (fromSceneName == Names.Scenes.World) {
				spawnPlace = GameObject.Find (Names.SpawnPlaces.Hall).transform.position;
			} else if (fromSceneName == Names.Scenes.ThemeRoom) {
				spawnPlace = GameObject.Find (Names.SpawnPlaces.Theme).transform.position;
			}
			if (Global.themeData != null) {
				GameObject.Find ("MonitorToTheme/Text").GetComponent<TextMesh> ().text = Global.themeData.name;
			}
			break;
		case Names.Scenes.ThemeRoom:
			if (fromSceneName == Names.Scenes.CourseRoom) {
				spawnPlace = GameObject.Find (Names.SpawnPlaces.Course).transform.position;
			} else {
				spawnPlace = GameObject.Find (Names.SpawnPlaces.Task).transform.position;
			}
			GameObject.Find ("MonitorToCourse/Text").GetComponent<TextMesh> ().text = Global.themeData.name;
			break;
		case Names.Scenes.Lecture:
			spawnPlace = GameObject.Find (Names.SpawnPlaces.Simple).transform.position;
			break;
		case Names.Scenes.Quiz:
			spawnPlace = GameObject.Find (Names.SpawnPlaces.Simple).transform.position;
			break;
		default:

			break;
		}
		return spawnPlace;
	}

	public static void ActivatePortals() {
		Debug.Log("exit portal " + SceneManager.GetActiveScene().name);
		string currentSceneName = SceneManager.GetActiveScene().name;
		string fromSceneName = Global.fromSceneName;
		switch (currentSceneName) {
		case Names.Scenes.World: 
			if (fromSceneName == Names.Scenes.CourseRoom || fromSceneName == Names.Scenes.ThemeRoom) {
				GameObject.Find (Names.Teleports.ToCourse).GetComponent<TeleportToScene> ().active = true;
			}
			break;
		case Names.Scenes.CourseRoom:
			if (Global.themeData != null) {
				GameObject.Find (Names.Teleports.ToTheme).GetComponent<TeleportToScene> ().active = true;
			}
			GameObject.Find (Names.Teleports.ToHall).GetComponent<TeleportToScene> ().active = true;
			break;
		case Names.Scenes.ThemeRoom:
			GameObject.Find (Names.Teleports.ToCourse).GetComponent<TeleportToScene> ().active = true;
			GameObject.Find (Names.Teleports.ToHall).GetComponent<TeleportToScene> ().active = true;
			break;
		default:
			//все комнаты с заданиями, тестовая и комната с лекциями
			GameObject.Find (Names.Teleports.Simple).GetComponent<TeleportToScene> ().active = true;
			break;
		}
	}
}
