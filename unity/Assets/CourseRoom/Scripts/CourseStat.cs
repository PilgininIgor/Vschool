using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CourseStat : MonoBehaviour {

	public GameObject statDisplay;

	private const string LBL1 = "Курс";

	void Start () {
		statDisplay = GameObject.Find ("StatDisplay").gameObject;

		HttpConnector httpConnector = GameObject.Find ("Bootstrap").GetComponent<HttpConnector> ();
		var parameters = new Dictionary<string, string> ();
		parameters ["id"] = Global.courseId;

		if (!Global.stat_loaded) {
			httpConnector.Post (HttpConnector.ServerUrl + HttpConnector.StatUrl, parameters, w => {
				Global.stat = JsonFx.Json.JsonReader.Deserialize<DataStructures.CourseRun> (w.text);
				Global.stat_loaded = true;
				statDisplay.transform.Find ("TextCourse").GetComponent<TextMesh> ().text = string.Format ("{0} \"{1}\"", LBL1, Global.courseName);
				var ts = TimeSpan.FromSeconds(Global.stat.timeSpent);
				statDisplay.transform.Find("TextTime").GetComponent<TextMesh>().text = string.Format("{0:00}:{1:00}:{2:00}", ts.TotalHours, ts.Minutes, ts.Seconds);
				statDisplay.transform.Find ("TextProgress").GetComponent<TextMesh> ().text = string.Format ("{0}%", Global.stat.progress);
			}, w => {
			});
		}
		statDisplay.transform.Find ("TextCourse").GetComponent<TextMesh> ().text = string.Format ("{0} \"{1}\"", LBL1, Global.courseName);
		var ts1 = TimeSpan.FromSeconds(Global.stat.timeSpent);
		statDisplay.transform.Find("TextTime").GetComponent<TextMesh>().text = string.Format("{0:00}:{1:00}:{2:00}", ts1.TotalHours, ts1.Minutes, ts1.Seconds);
		statDisplay.transform.Find ("TextProgress").GetComponent<TextMesh> ().text = string.Format ("{0}%", Global.stat.progress);
	}
}
