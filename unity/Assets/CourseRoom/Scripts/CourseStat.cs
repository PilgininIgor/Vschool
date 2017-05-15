using System.Collections;
using System.Collections.Generic;
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
			}, w => {
			});
		}
		statDisplay.transform.Find ("TextCourse").GetComponent<TextMesh> ().text = string.Format ("{0} \"{1}\"", LBL1, Global.courseName);
		statDisplay.transform.Find ("TextProgress").GetComponent<TextMesh> ().text = string.Format ("{0}%", Global.stat.progress);
	}
}
